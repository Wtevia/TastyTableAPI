using System.ComponentModel.DataAnnotations;

namespace Core.CredentialModels
{
    public class ChangePasswordModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Length of OldPassword is out of bounds")]
        [RegularExpression("^(?=.*\\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,20}$")]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Length of NewPassword is out of bounds")]
        [RegularExpression("^(?=.*\\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,20}$")]
        public string NewPassword { get; set; }
    }
}