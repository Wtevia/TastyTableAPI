using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Models;

public class Chat : BaseEntity
{
    public string Title { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ChatState State { get; set; }

    public virtual List<Message> Messages { get; } = new();
    
    public virtual List<ChatUser> UserChats { get; } = new();
    
    public virtual List<User> Users { get; } = new();
}