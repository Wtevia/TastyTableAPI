using Microsoft.IdentityModel.Tokens;

namespace Core;

public static class Consts
{
    public const int UsernameMinLength = 5;

    public const string PasswordRegex = @"^(?=.*[A-Z])(?=.*[\W])(?=.*[0-9])(?=.*[a-z]).{8,128}$";
    public const string PasswordValidationError = "Password must have more than 6 characters, min. 1 uppercase, min. 1 lowercase, min. 1 special characters.";

    public const string UsernameLengthValidationError = "Username must have more than 5 characters.";
    public const string EmailValidationError = "Email must have valid format.";

    public class LoginProviders
    {
        public const string Google = "GOOGLE";
        // public const string Facebook = "FACEBOOK";
        // public const string Password = "PASSWORD";
        
        public static bool IsValid(string role)
        {
            return role.Equals(Google);
        }
    }
    
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Deliverer = "Deliverer";

        public static bool IsValid(string role)
        {
            return role.Equals(Admin) || role.Equals(Customer) || role.Equals(Deliverer);
        }
    }
}