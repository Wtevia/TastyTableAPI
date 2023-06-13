using System;
using System.ComponentModel.DataAnnotations;

namespace Core.CredentialModels
{
    public class RegisterUserModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "UserName's length must be greater or equal to2")]
        public string UserName { get; set; }
     
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
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
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password's length must be greater or equal to 8")]
        [RegularExpression(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,20}$")]
        public string Password { get; set; }

        [Required] 
        [RegularExpression($"{Consts.UserRoles.Customer}|{Consts.UserRoles.Deliverer}", 
            ErrorMessage = "Invalid role")]
        public string Role { get; set; } = Consts.UserRoles.Customer;
    }
}