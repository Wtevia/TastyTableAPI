using Core;
using Core.Enums;
using Core.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        
        var dishes = new List<Dish>();
        var random = new Random();

        string[] drinkNames = { "Coca-Cola", "Sprite", "Iced Tea", "Orange Juice", "Coffee", "Milkshake", "Lemonade", "Pepsi", "Fruit Punch", "Smoothie" };
        string[] fastFoodNames = { "Hamburger", "Cheeseburger", "Hot Dog", "French Fries", "Pizza", "Chicken Nuggets", "Taco", "Burrito", "Sub Sandwich", "Quesadilla" };
        string[] saladNames = { "Caesar Salad", "Greek Salad", "Cobb Salad", "Caprese Salad", "Spinach Salad", "Chicken Salad", "Fruit Salad", "Quinoa Salad", "Pasta Salad", "Tuna Salad" };

        for (int i = 0; i < 10; i++)
        {
            dishes.Add(new Dish()
            {
                Category = DishCategory.Drinks,
                Name = drinkNames[i],
                Price = random.Next(1, 10) + random.Next(0, 100) / 100.0f,
                Description = "This is a refreshing beverage.",
                IsAvailable = true,
                Image = "https://url_to_image"
            });

            dishes.Add(new Dish()
            {
                Category = DishCategory.FastFood,
                Name = fastFoodNames[i],
                Price = random.Next(5, 15) + random.Next(0, 100) / 100.0f,
                Description = "This is a delicious fast food item.",
                IsAvailable = true,
                Image = "https://url_to_image"
            });

            dishes.Add(new Dish()
            {
                Category = DishCategory.Salads,
                Name = saladNames[i],
                Price = random.Next(5, 12) + random.Next(0, 100) / 100.0f,
                Description = "This is a healthy salad option.",
                IsAvailable = true,
                Image = "https://url_to_image"
            });
        }

        context.Dishes.AddRange(dishes);
        await context.SaveChangesAsync();
        
        Console.WriteLine("Created dishes count: " + dishes.Count);
    }
}