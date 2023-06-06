using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Models;

public class Order : BaseEntity
{
    public User User { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string Address { get; set; }
    
    public bool IsPayed { get; set; }
    
    public OrderState State { get; set; }
    
    public virtual List<DishOrder> OrderDishes { get; } = new();
    
    public virtual List<Dish> Dishes { get; } = new();
}