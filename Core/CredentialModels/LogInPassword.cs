using System.ComponentModel.DataAnnotations;

namespace Core.CredentialModels
{
    public class LogInPassword
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = Consts.EmailValidationError)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordValidationError)]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{Email} {Password}";
        }
    }
}