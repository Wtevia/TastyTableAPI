using Core;
using DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DAL
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var connectionString = services.BuildServiceProvider()
                .GetRequiredService<IOptions<AppSettings>>().Value.DefaultConnection;
            
            services.AddDbContext<RestaurantContext>(
                builder => builder.UseMySql(connectionString , ServerVersion.AutoDetect(connectionString)),
                ServiceLifetime.Transient);
        }
    }
}