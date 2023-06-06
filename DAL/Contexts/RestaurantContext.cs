using System.Security.AccessControl;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public sealed class RestaurantContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ExternalAuth> ExternalAuths { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<ChatUser> ChatUsers { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Dish> Dishes { get; set; } = null!;
        public DbSet<DishOrder> DishOrders { get; set; } = null!;
        
        
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatUser>()
                .HasAlternateKey(uc => new {uc.UserId, uc.ChatId});
            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users)
                .UsingEntity<ChatUser>();

            modelBuilder.Entity<DishOrder>()
                .HasAlternateKey(od => new {od.OrderId, od.DishId});
            modelBuilder.Entity<Dish>()
                .HasMany(d => d.Orders)
                .WithMany(o => o.Dishes)
                .UsingEntity<DishOrder>();

            modelBuilder.Entity<ExternalAuth>()
                .HasAlternateKey(ea => ea.Key);
            
        }
    }
}