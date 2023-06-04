using Core.Enums;

namespace Core.Models;

public class Dish : BaseEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public float Price { get; set; }
    
    public string Image { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public DishCategory Category { get; set; }
    
    public virtual List<DishOrder> OrderDishes { get; } = new();
    
    public virtual List<Order> Orders { get; } = new();
}