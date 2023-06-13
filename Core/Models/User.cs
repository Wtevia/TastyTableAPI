using System.ComponentModel.DataAnnotations;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class User : IdentityUser<int>
{
    public User() : base() { }

    public User(string userName) : base(userName) { }

    public DateTime? DateOfBirth { get; set; } = null;

    public string? Address { get; set; }
    
    // public virtual List<Role> Roles { get; set; }

    public virtual List<ExternalUserLogin> ExternalUserLogins { get; } = new();
    
    public virtual List<Order> Orders { get; } = new();

    public virtual List<ChatUser> UserChats { get; } = new();

    public virtual List<Chat> Chats { get; } = new();
}