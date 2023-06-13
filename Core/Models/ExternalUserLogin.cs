using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class ExternalUserLogin : IdentityUserLogin<int>
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}