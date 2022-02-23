using API.LUX.MockSetup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.LUX.Configurations
{
    internal static class MockConfiguration
    {
        public static IServiceCollection AddMockConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //services.AddSingleton(configuration.GetSection("MockSetup:TaxaJurosMockSetup").Get<TaxaJurosMockSetup>());

            return services;
        }
    }
}
