using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace API.LUX.Configurations.Swagger
{
    internal class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            // Add a swagger document for each discovered API version
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            var commentFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var commentFilePath = Path.Combine(AppContext.BaseDirectory, commentFileName);
            options.IncludeXmlComments(commentFilePath);
            options.ExampleFilters();
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "API.Juros",
                Version = $"v{description.ApiVersion}",
                Description = "APIs - API.Juros",
                Contact = new OpenApiContact
                {
                    Name = "API.Juros"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " - Essa versão da API será descontinuada.";
            }

            return info;
        }
    }
}
