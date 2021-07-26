using System;
using BookStore.Infrastructure.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.API.Configurations
{
    public static class DependencyInjectionConfig
    {
         public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            IoCInjector.RegisterServices(services);
        }
    }
}