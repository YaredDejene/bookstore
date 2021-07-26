using System;
using BookStore.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.API.Configurations
{
    public static class MappingConfig
    {
        public static void AddMappingConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToModelProfile), typeof(ModelToDomainProfile));
        }
    }
}