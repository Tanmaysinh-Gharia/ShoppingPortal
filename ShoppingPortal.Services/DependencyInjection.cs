using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Interfaces;
using ShoppingPortal.Data.Repositories;
using ShoppingPortal.Services.ProductServices;
using ShoppingPortal.Services.UserServices;

namespace ShoppingPortal.Services
{
    public static class DependencyInjection
    {
        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

            // Add this in your service configuration section
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

        }

    }
}
