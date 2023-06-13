using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Core.CredentialModels;

public class SocialLogin
{
    [Required]
    [RegularExpression($"{Consts.LoginProviders.Google}", 
        ErrorMessage = "Invalid provider")]
    public string Provider { get; set; }

    [RegularExpression($"{Consts.UserRoles.Customer}|{Consts.UserRoles.Deliverer}", 
        ErrorMessage = "Invalid role")]
    public string? Role { get; set; } = Consts.UserRoles.Customer;

    public string? AccessToken { get; set; }
}