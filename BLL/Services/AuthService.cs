using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using BLL.Interfaces;
using Core;
using Core.CredentialModels;
using Core.Enums;
using Core.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using FluentResults;
using Google.Apis.Auth;

namespace BLL.Services;

public class AuthService : IAuthService
{
    private RestaurantContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    
    public AuthService(IConfiguration configuration, UserManager<User> userManager,
        RestaurantContext restaurantContext)
    {
        _dbContext = restaurantContext;
        _configuration = configuration;
        _userManager = userManager;
    }
    
    public async Task<JwtTokens> GenerateJwtTokens(User user)
    {
        var claims = await GetAuthClaims(user);
        var jwtToken = GetJwtToken(claims);
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new JwtTokens()
        {
            AccessToken = encodedJwt,
            ExpiresAtTimestamp = jwtToken.Payload.Exp,
            UserId = user.Id,
            UserEmail = user.Email,
            UserRoles = await _userManager.GetRolesAsync(user)
        };
    }
    
    public async Task<User> PasswordLogIn(LogInPassword logInPassword)
    {
        var user = await _userManager.FindByEmailAsync(logInPassword.Email);
        
        var passwordIsCorrect = user != null && await _userManager.CheckPasswordAsync(user, logInPassword.Password);

        if (!passwordIsCorrect)
        {
            throw new Exception("User with such email and password doesn't exist.");
        }

        return user;
    }

    public async Task<User> RegisterUser(RegisterUserModel registerModel)
    {
        var user = new User()
        {
            UserName = registerModel.UserName,
            Email = registerModel.Email,
            EmailConfirmed = true,
            PhoneNumber = registerModel.Phone,
            Address = registerModel.Address,
            DateOfBirth = registerModel.DateOfBirth,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        var result = registerModel.Password is null ? await _userManager.CreateAsync(user) 
            : await _userManager.CreateAsync(user, registerModel.Password);
        if(!result.Succeeded)
        {
            throw new Exception(
                $"Unable to register user {registerModel.Email}, errors: {GetErrorsText(result.Errors)}");
        }
        
        var res2 = await _userManager.AddToRoleAsync(user, registerModel.Role);
        Console.WriteLine($"Role attached: {res2.Succeeded} , Errors: {GetErrorsText(res2.Errors)}");

        return user;
    }

    public async Task<User> SocialLoginOrRegister(SocialLogin socialLogin, SocialTokenValidationResult validationResult)
    {
        var email = validationResult.Email;
        var provider = validationResult.Provider ?? socialLogin.Provider;
        var user = await _userManager.FindByLoginAsync(provider, validationResult.ProviderKey);

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(email);
        }

        if (user is null)
        {
            user = await RegisterUser(new RegisterUserModel
            {
                Email = email,
                UserName = validationResult.Name,
                Role = socialLogin.Role ?? Consts.UserRoles.Customer
            });

            await _userManager.AddLoginAsync(user, new UserLoginInfo(
                loginProvider: provider,
                providerKey: validationResult.ProviderKey,
                displayName: provider));
        }

        return user;
    }

    private JwtSecurityToken GetJwtToken(IEnumerable<Claim> authClaims)
    {
        var expiresAt = DateTime.Now.Add(TimeSpan.FromDays(1));
        var jwt = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: authClaims,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), 
                SecurityAlgorithms.HmacSha256));

        return jwt;
    }

    private string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(error => error.Description).ToArray());
    }

    /// <summary>
    /// Validate AccessToken and return info about authorized user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Result<SocialTokenValidationResult>> ValidateSocialToken(SocialLogin request)
    {
        SocialTokenValidationResult res;
        switch (request.Provider)
        {
            // case Consts.LoginProviders.Facebook:
            //     var respF = await ValidateFacebookToken(request);
            //     email = respF.Value.Email;
            //     break;
            case Consts.LoginProviders.Google:
                var respG = await ValidateGoogleToken(request);
                res = new SocialTokenValidationResult
                {
                    Name = respG.Value.Name,
                    Email = respG.Value.Email,
                    Provider = request.Provider,
                    ProviderKey = respG.Value.JwtId
                };
                break;
            default:
                return Result.Fail($"{request.Provider} provider is not supported.");
        }

        return Result.Ok(res);
    }

    // private async Task<Result> ValidateFacebookToken(SocialLoginRequest request)
    // {
    //     var httpClient = _httpClientFactory.CreateClient();
    //     var appAccessTokenResponse = await httpClient.GetFromJsonAsync<FacebookAppAccessTokenResponse>($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["SocialLogin:Facebook:ClientId"]}&client_secret={_configuration["SocialLogin:Facebook:ClientSecret"]}&grant_type=client_credentials");
    //     var response =
    //         await httpClient.GetFromJsonAsync<FacebookTokenValidationResult>(
    //             $"https://graph.facebook.com/debug_token?input_token={request.AccessToken}&access_token={appAccessTokenResponse!.AccessToken}");
    //
    //     if (response is null || !response.Data.IsValid)
    //     {
    //         return Result.Fail($"{request.Provider} access token is not valid.");
    //     }
    //     
    //     return Result.Ok();
    // }

    private async Task<Result<GoogleJsonWebSignature.Payload>> ValidateGoogleToken(SocialLogin request)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _configuration["GoogleAuth:ClientId"] }
            };
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(
                request.AccessToken, settings);

            Console.WriteLine($"Google audience: {payload.Audience} , name: {payload.Name} , " +
                              $"email: {payload.Email}");
            
            return Result.Ok(payload);
        }
        catch (InvalidJwtException ex)
        {
            throw;
            return Result.Fail($"{request.Provider} access token is not valid.");
        }
    }

    private async Task<List<Claim>> GetAuthClaims(User user)
    {
        var authClaims = new List<Claim>
        {
            new("UserId", user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email!)
        };
        
        var userRoles = await _userManager.GetRolesAsync(user);

        if (userRoles is not null && userRoles.Any())
        {
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
        }

        return authClaims;
    }
}