using Core;
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
}