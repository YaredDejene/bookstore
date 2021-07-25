using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Identity;
using NetDevPack.Identity.Jwt;

namespace BookStore.Infrastructure.Identity
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityEntityFrameworkContextConfiguration(options => 
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("BookStore.Infrastructure.Identity")));
            

            // Default Identity configuration from NetDevPack.Identity
            services.AddIdentityConfiguration();

            // JWT
            services.AddJwtConfiguration(configuration, "AppSettings");
        }
    }
}
