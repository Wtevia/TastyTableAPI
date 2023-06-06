using Core.Enums;

namespace Core.Models;

public class User : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string? Phone { get; set; }

    public string Password { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Address { get; set; }

    public UserRole UserRole { get; set; } = UserRole.Customer;

    public virtual List<Order> Orders { get; } = new();

    public virtual List<ChatUser> UserChats { get; } = new();

    public virtual List<Chat> Chats { get; } = new();
}