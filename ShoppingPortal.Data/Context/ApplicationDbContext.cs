using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingPortal.Data.Configurations;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShoppingPortal.Core.Helpers;

using System.Security.Cryptography;
using ShoppingPortal.Data.Seeds;

namespace ShoppingPortal.Data.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        //Setting up tables 
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatusLog> OrderStatusLogs { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        //Applying Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data - now using static values
            modelBuilder.Entity<Address>().HasData(AddressSeedData.GetSeedAddress());
            modelBuilder.Entity<User>().HasData(UserSeedData.GetSeedUsers());
            modelBuilder.Entity<Category>().HasData(CategorySeedData.GetSeedCategories());
            modelBuilder.Entity<Product>().HasData(ProductSeedData.GetSeedProducts());
            modelBuilder.Entity<ShoppingCart>().HasData(ShoppingCartSeedData.GetSeedShoppingCarts());
            modelBuilder.Entity<CartItem>().HasData(CartItemSeedData.GetSeedCartItems());
            modelBuilder.Entity<Order>().HasData(OrderSeedData.GetSeedOrders());
            modelBuilder.Entity<OrderItem>().HasData(OrderItemSeedData.GetSeedOrderItems());
            modelBuilder.Entity<OrderStatusLog>().HasData(OrderStatusLogSeedData.GetSeedOrderStatusLogs());
            modelBuilder.Entity<ProductCategory>().HasData(ProductCategorySeedData.GetSeedProductCategories());




            //Configuring Models
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusLogConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseLazyLoadingProxies(); // Enable lazy loading
        //}
        // Add helper methods for explicit loading
        public async Task LoadReferenceAsync<TEntity, TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity : class
            where TProperty : class
        {
            await Entry(entity).Reference(navigationProperty).LoadAsync();
        }

        public async Task LoadCollectionAsync<TEntity, TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> navigationProperty)
            where TEntity : class
            where TProperty : class
        {
            await Entry(entity).Collection(navigationProperty).LoadAsync();
        }

        // Method for selective lazy loading
        public User EnableLazyLoadingForUser(Guid userId)
        {
            try
            {
                this.ChangeTracker.LazyLoadingEnabled = true;
                var user = Users.Find(userId);

                // Accessing these will now trigger lazy loading
                var address = user.Address;
                var orders = user.Orders;

                return user;
            }
            finally
            {
                this.ChangeTracker.LazyLoadingEnabled = false;
            }
        }


    }
}
