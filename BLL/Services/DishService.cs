using Core.Enums;
using Core.Models;
using DAL.Contexts;
using Microsoft.Extensions.Configuration;
using TastyTableAPI;

namespace BLL.Services;

public class DishService
{
    private RestaurantContext _context;
    private readonly IConfiguration _configuration;
    
    public DishService(IConfiguration configuration, RestaurantContext restaurantContext)
    {
        _context = restaurantContext;
        _configuration = configuration;
    }
    
    public IEnumerable<Dish> SearchDishes(SearchDishFilters filters)
    {
        var dishes = _context.Set<Dish>().AsEnumerable()
                .Where(d => SearchDishesFiltersValidation(d, filters));
        
        return dishes;
    }

    private bool SearchDishesFiltersValidation(Dish dish, SearchDishFilters filters)
    {
        var isSuccess = true;

        if (!filters.Substring.Equals(""))
        {
            var substring = filters.Substring.ToLower();
            isSuccess &= dish.Name.ToLower().Contains(substring) ||
                         dish.Description.ToLower().Contains(substring);
        }
                
        if (filters.DishCategory is not null)
        {
            var filterCategory = Enum.Parse<DishCategory>(filters.DishCategory);
            isSuccess &= dish.Category == filterCategory;
        }

        isSuccess &= dish.Price < filters.MaxPrice && dish.Price > filters.MinPrice;

        return isSuccess;
    }
}