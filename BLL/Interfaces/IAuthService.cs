using Core.CredentialModels;
using Core.Models;

namespace BLL.Interfaces;

public interface IAuthService
{
    JwtTokens GenerateJwtTokens(User user);

    User? LogIn(LogInModel logInModel);
    
    User RegisterUser(RegisterUserModel logInModel);
}