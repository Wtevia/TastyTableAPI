using System.ComponentModel.DataAnnotations;

namespace Core.CredentialModels
{
    public class RegisterUserModel
    {
        [Required]
        [StringLength(128, MinimumLength = Consts.UsernameMinLength, ErrorMessage = Consts.UsernameLengthValidationError)]
        public string UserName { get; set; }
     
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = Consts.EmailValidationError)]
        public string Email { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string? Phone { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordValidationError)]
        public string Password { get; set; }

        [Required] 
        [RegularExpression($"{Consts.UserRoles.Customer}|{Consts.UserRoles.Deliverer}", 
            ErrorMessage = "Invalid role")]
        public string Role { get; set; } = Consts.UserRoles.Customer;
    }
}