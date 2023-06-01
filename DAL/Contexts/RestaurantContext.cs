using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public sealed class RestaurantContext : DbContext
    {
        
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}