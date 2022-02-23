using Domain.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Domain.LUX.Mapper
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            service.AddSingleton(MappingsConfig.RegisterMappings().CreateMapper());
        }
    }
}
