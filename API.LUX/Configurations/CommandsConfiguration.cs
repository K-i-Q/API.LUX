using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.Juros.Configurations
{
    internal static class CommandsConfiguration
    {
        public static IServiceCollection AddCommandsConfiguration(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //services.AddScoped<ITaxaJurosCommandHandler, TaxaJurosCommandHandler>();


            return services;
        }
    }
}
