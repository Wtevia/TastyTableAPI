using System.IO;
using DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(/*"B:\\NIXTraining\\Messenger\\Application\\PresentationLayer"*/Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false)
                    .AddEnvironmentVariables()
                    .Build();

            services.AddTransient<Core.AppSettings>();
            services.Configure<Core.AppSettings>(configuration.GetSection("AppSettings"));
            services.AddScoped(builder => configuration.GetSection("AppSettings").Get<Core.AppSettings>());

            var connectionString = services.BuildServiceProvider()
                .GetRequiredService<Core.AppSettings>().DefaultConnection;
            services.AddDbContext<MessengerContext>(
                builder => builder.UseMySql(connectionString , ServerVersion.AutoDetect(connectionString)),
                ServiceLifetime.Transient);
        }
    }
}