namespace Core.Models;

public class DishOrder : BaseEntity
{
    public int OrderId { get; set; }
    
    public int DishId { get; set; }
    
    public virtual Order Order { get; set; }
    
    public virtual Dish Dish { get; set; }

    public int Count { get; set; }

    public float GetPrice()
    {
        return this.Count * this.Dish.Price;
    }
}