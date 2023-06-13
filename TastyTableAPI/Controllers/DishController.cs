using BLL.Services;
using Core.Enums;
using Core.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TastyTableAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DishController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly RestaurantContext _context;
    private readonly DishService _dishService;

    public DishController(IConfiguration configuration, ILogger<AuthController> logger,
        RestaurantContext context, DishService dishService)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
        _dishService = dishService;
    }

    [HttpGet("categories")]
    public async Task<ActionResult<List<string>>> GetDishesCategories()
    {
        var categories = new List<string>()
        {
            DishCategory.Drinks.ToString(),
            DishCategory.Salads.ToString(),
            DishCategory.FastFood.ToString(),
        };

        return Ok(categories);
    }
    
    [HttpPost("search")]
    public async Task<ActionResult<List<Dish>>> SearchDishes([FromBody]SearchDishFilters filters)
    {
        var dishes = _dishService.SearchDishes(filters);
            // .Select(d => new
            // {
            //     d.Name,
            //     d.Description,
            //     d.Price,
            //     d.Image,
            //     d.IsAvailable,
            //     Category = d.Category.ToString(),
            //     d.Id
            // });
        return Ok(dishes);
    }
}