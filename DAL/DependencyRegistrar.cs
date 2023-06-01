using System.IO;
using DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var connectionString = services.BuildServiceProvider()
                .GetRequiredService<Core.AppSettings>().DefaultConnection;
            
            services.AddDbContext<RestaurantContext>(
                builder => builder.UseMySql(connectionString , ServerVersion.AutoDetect(connectionString)),
                ServiceLifetime.Transient);
        }
    }
}