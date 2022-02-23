using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.Juros.Configurations
{
    public static class QueriesConfiguration
    {
        public static IServiceCollection AddQueriesConfiguration(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //services.AddSingleton<ITaxaJurosQueryHandler, TaxaJurosQueryHandler>();


            return services;
        }
    }
}