using System.Security.AccessControl;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public sealed class RestaurantContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<ExternalUserLogin> ExternalUserLogins { get; set; } = null!;
        
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<ChatUser> ChatUsers { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Dish> Dishes { get; set; } = null!;
        public DbSet<DishOrder> DishOrders { get; set; } = null!;
        
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishOrder>()
                .HasAlternateKey(od => new {od.OrderId, od.DishId});
            modelBuilder.Entity<Dish>()
                .HasMany(d => d.Orders)
                .WithMany(o => o.Dishes)
                .UsingEntity<DishOrder>();

            
            modelBuilder.Entity<ChatUser>()
                .HasAlternateKey(uc => new {uc.UserId, uc.ChatId});
            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users)
                .UsingEntity<ChatUser>();
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Email);

            modelBuilder.Entity<UserRole>().HasKey(
                p => new { p.UserId, p.RoleId });

            modelBuilder.Entity<ExternalUserLogin>().HasAlternateKey(
                p => new { p.UserId, p.LoginProvider });
        }
    }
}