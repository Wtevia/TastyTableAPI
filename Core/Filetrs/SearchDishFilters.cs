using Core.Enums;

namespace TastyTableAPI;

public class SearchDishFilters
{
    public string Substring { get; set; } = "";
    public string? DishCategory { get; set; } = null;
    public int MinPrice { get; set; } = 0;
    public int MaxPrice { get; set; } = int.MaxValue;
}