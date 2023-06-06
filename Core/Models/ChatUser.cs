namespace Core.Models;

public class ChatUser : BaseEntity
{
    public int UserId { get; set; }
    
    public int ChatId { get; set; }
    
    public virtual User User { get; set; }
    
    public virtual Chat Chat { get; set; }
}