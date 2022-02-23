using Domain.LUX.Mapper;
using Domain.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.LUX.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddAutoMapperSetup();

            return services;
        }
    }
}
