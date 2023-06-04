using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Message : BaseEntity
{
    public string Text { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual Chat Chat { get; set; }
}