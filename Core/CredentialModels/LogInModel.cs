using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Core.Models;

namespace Core.CredentialModels
{
    public class LogInModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password's length must be greater or equal to 8")]
        [RegularExpression(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,20}$")]
        public string Password { get; set; }
        
        public override string ToString()
        {
            var data = $"{Email} {Password}";
            return data;
        }
    }
}