using Library.Shared.Constants;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Library.API.Configurations
{
    public class SwaggerConfigService() : IConfigureNamedOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "API token needed to access the endpoints",
                Type = SecuritySchemeType.ApiKey,
                Name = AuthConstants.ApiKeyHeaderName,
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header,
            };

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { scheme, new List<string>() }
            });

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Library API",
                Description = "A simple API for managing a libray system.",
            });

            options.EnableAnnotations();

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}
