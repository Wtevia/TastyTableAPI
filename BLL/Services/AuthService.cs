using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Interfaces;
using Core.CredentialModels;
using Core.Enums;
using Core.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services;

public class AuthService : IAuthService
{
    private RestaurantContext _dbContext;
    private readonly IConfiguration _configuration;
    
    public AuthService(IConfiguration configuration, RestaurantContext restaurantContext)
    {
        _dbContext = restaurantContext;
        _configuration = configuration;
    }
    
    public JwtTokens GenerateJwtTokens(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserRole.ToString())
        };

        var expiresAt = DateTime.Now.Add(TimeSpan.FromHours(3));
        var jwt = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), 
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JwtTokens()
        {
            AccessToken = encodedJwt,
            AccessTokenExpiresAt = expiresAt
        };
    }
    
    public User? LogIn(LogInModel logInModel)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email.Equals(logInModel.Email));

        var hasher = new PasswordHasher<User>();
        var paswordIsCorrect = false;
        if (user != null)
        {
            paswordIsCorrect = hasher.VerifyHashedPassword(
                user: user, 
                hashedPassword: user.PasswordHash, 
                providedPassword: logInModel.Password) == PasswordVerificationResult.Success;
        }
        
        if (!paswordIsCorrect)
        {
            throw new ArgumentException("User with such login and password doesn't exist.");
        }

        return user;
    }

    public User RegisterUser(RegisterUserModel registerModel)
    {
        var pwdHasher = new PasswordHasher<User>();
        var user = new User()
        {
            Name = registerModel.Name,
            Email = registerModel.Email,
            Phone = registerModel.Phone,
            Address = registerModel.Address,
            DateOfBirth = registerModel.DateOfBirth,
            UserRole = registerModel.IsDeliverer ? UserRole.Deliverer : UserRole.Customer
        };

        string passwordHash = pwdHasher.HashPassword(user, registerModel.Password);
        user.PasswordHash = passwordHash;
        
        _dbContext.Users.Attach(user);
        
        _dbContext.SaveChanges();
        
        return user;
    }
}