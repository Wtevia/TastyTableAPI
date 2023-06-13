using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.CredentialModels;
using Core.Models;
using FluentResults;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces;

public interface IAuthService
{
    Task<JwtTokens> GenerateJwtTokens(User user);

    Task<User> PasswordLogIn(LogInPassword logInPassword);

    Task<User> RegisterUser(RegisterUserModel registerModel);

    Task<User> SocialLoginOrRegister(SocialLogin socialLogin, SocialTokenValidationResult validationResult);

    Task<Result<SocialTokenValidationResult>> ValidateSocialToken(SocialLogin request);
}