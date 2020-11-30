using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;

namespace GameLoanManager.Api.Swagger
{
    /// <summary>
    /// SwaggerConfig
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        ///     Configure services for swagger.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void ConfigureSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var swagger = configuration.GetSection(nameof(SwaggerDoc)).Get<SwaggerDoc>() ?? new SwaggerDoc();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("../swagger/v1/swagger.json", swagger.EndpointName);
                s.EnableFilter();
                s.RoutePrefix = "swagger";
                s.DocExpansion(DocExpansion.List);
            });
        }

        /// <summary>
        ///     Add swagger configurations services.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration, string assemblyPath)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrEmpty(assemblyPath))
                throw new ArgumentNullException(nameof(assemblyPath));

            var swagger = configuration.GetSection(nameof(SwaggerDoc)).Get<SwaggerDoc>() ?? new SwaggerDoc();

            var realPathXmlDoc =
                $"{Path.GetDirectoryName(assemblyPath)}{Path.DirectorySeparatorChar}{Path.GetFileNameWithoutExtension(assemblyPath)}.xml";

            services.AddSwaggerGen(GetSwaggerOptions(swagger, realPathXmlDoc));
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            return services;
        }

        private static Action<SwaggerGenOptions> GetSwaggerOptions(SwaggerDoc swagger, string pathXmlDoc)
        {
            if (swagger == null)
                throw new ArgumentNullException(nameof(swagger));

            if (string.IsNullOrEmpty(pathXmlDoc))
                throw new ArgumentNullException(nameof(pathXmlDoc));

            return c =>
            {
                c.SwaggerDoc(swagger.Version,
                    new OpenApiInfo
                    {
                        Title = swagger.Title,
                        Version = swagger.Version,
                        Description = swagger.Description
                    });

                c.ExampleFilters();
                c.IncludeXmlComments(pathXmlDoc);

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            };
        }

    }
}
