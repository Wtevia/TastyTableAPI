using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class UserRole : IdentityUserRole<int>
{
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
}