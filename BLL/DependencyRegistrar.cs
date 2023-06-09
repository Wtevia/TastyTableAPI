using BLL.Interfaces;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<DishService>();
            services.AddTransient<IUserService, UserService>();
            
            DAL.DependencyRegistrar.ConfigureServices(services);
        }
    }
}