using Core;
using Core.Enums;
using Core.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TastyTableAPI;

public class SeedManager
{
    public static async Task Seed(IServiceProvider services)
    {
        // await SeedRoles(services);
        
        // await SeedAdminUser(services);

        // await SeedDishes(services);
    }

    private static async Task SeedRoles(IServiceProvider services)
    {
        var context = services.GetRequiredService<RestaurantContext>();
        var roleManager = services.GetRequiredService<RoleManager<Role>>();

        await roleManager.CreateAsync(new Role(Consts.UserRoles.Admin));
        await roleManager.CreateAsync(new Role(Consts.UserRoles.Customer));
        await roleManager.CreateAsync(new Role(Consts.UserRoles.Deliverer));

        Console.WriteLine("SeedRoles count: " + roleManager.Roles.Select(r => r.Name).Count());
    }
    
    private static async Task SeedAdminUser(IServiceProvider services)
    {
        var context = services.GetRequiredService<RestaurantContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();

        var adminUser = await context.Users.FirstOrDefaultAsync(user => user.UserName == "AuthAdmin");
        
        if (adminUser is null)
        {
            adminUser = new User { UserName = "AuthAdmin", Email = "admin@gmail.com" };
            await userManager.CreateAsync(adminUser, "VerySecretPassword!1");
            Console.WriteLine("ADMIN user id: " + adminUser.Id);
            var resp = await userManager.AddToRoleAsync(adminUser, Consts.UserRoles.Admin);
            Console.WriteLine("ADD role result: " + resp.Succeeded);
        }
    }
    
    private static async Task SeedDishes(IServiceProvider services)
    {
        var context = services.GetRequiredService<RestaurantContext>();

        var dishes = GetDishes();
        
        context.Dishes.AddRange(dishes);
        await context.SaveChangesAsync();
        
        Console.WriteLine("Created dishes count: " + dishes.Count);
    }

    private static IList<Dish> GetDishes()
    {
        var dishes = new List<Dish>()
        {
            // Drinks
            new Dish()
            {
                Category = DishCategory.Drinks,
                Name = "Coca-Cola",
                Price = 2.5f,
                Description = "A classic carbonated beverage.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Drinks,
                Name = "Iced Tea",
                Price = 1.75f,
                Description = "Refreshing tea served over ice.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Drinks,
                Name = "Orange Juice",
                Price = 2.0f,
                Description = "Freshly squeezed orange juice.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Drinks,
                Name = "Coffee",
                Price = 2.25f,
                Description = "A hot beverage made from roasted coffee beans.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Drinks,
                Name = "Milkshake",
                Price = 3.0f,
                Description = "Creamy and indulgent blended milk and ice cream.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Drinks,
                Name = "Lemonade",
                Price = 1.5f,
                Description = "Tangy and refreshing lemon-flavored drink.",
                IsAvailable = true,
                Image = string.Empty
            },
            
            // Fast Food
            new Dish()
            {
                Category = DishCategory.FastFood,
                Name = "Hamburger",
                Price = 4.99f,
                Description = "Juicy beef patty served on a bun with toppings.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.FastFood,
                Name = "Pizza",
                Price = 9.99f,
                Description = "Cheesy goodness on a crispy crust.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.FastFood,
                Name = "Hot Dog",
                Price = 3.5f,
                Description = "A grilled sausage served in a bun with condiments.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.FastFood,
                Name = "French Fries",
                Price = 2.75f,
                Description = "Crispy and seasoned potato fries.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.FastFood,
                Name = "Chicken Nuggets",
                Price = 4.5f,
                Description = "Breaded and fried bite-sized pieces of chicken.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.FastFood,
                Name = "Taco",
                Price = 3.25f,
                Description = "A Mexican dish consisting of a tortilla filled with various ingredients.",
                IsAvailable = true,
                Image = string.Empty
            },
            
            // Salads
            new Dish()
            {
                Category = DishCategory.Salads,
                Name = "Caesar Salad",
                Price = 6.99f,
                Description = "Fresh romaine lettuce with Caesar dressing and croutons.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Salads,
                Name = "Greek Salad",
                Price = 7.99f,
                Description = "Crisp vegetables and feta cheese in a tangy dressing.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Salads,
                Name = "Caprese Salad",
                Price = 8.5f,
                Description = "Fresh tomatoes, mozzarella cheese, and basil with balsamic glaze.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Salads,
                Name = "Spinach Salad",
                Price = 6.5f,
                Description = "Tender spinach leaves with sliced almonds and vinaigrette.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Salads,
                Name = "Chicken Salad",
                Price = 7.25f,
                Description = "Grilled chicken breast on a bed of mixed greens with dressing.",
                IsAvailable = true,
                Image = string.Empty
            },
            new Dish()
            {
                Category = DishCategory.Salads,
                Name = "Fruit Salad",
                Price = 5.5f,
                Description = "Assorted fresh fruits served in a light syrup.",
                IsAvailable = true,
                Image = string.Empty
            }
        };

        return dishes;
    }
}