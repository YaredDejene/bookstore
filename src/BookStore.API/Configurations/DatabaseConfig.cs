using System;
using BookStore.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.API.Configurations
{
    public static class DatabaseConfig
    {
         public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<BookStoreContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(EventStoreSqlContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention());

            services.AddDbContext<EventStoreSqlContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(EventStoreSqlContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention());
        }
    }
}