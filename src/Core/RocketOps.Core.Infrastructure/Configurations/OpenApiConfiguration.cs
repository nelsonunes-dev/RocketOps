using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RocketOps.Core.Infrastructure.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RocketOps.Core.Infrastructure.Configurations;

public static class OpenApiConfiguration
{
    public static IServiceCollection AddOpenApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind options from configuration
        services.Configure<OpenApiOption>(configuration.GetSection(OpenApiOption.ConfigurationKey));

        // Get options
        var options = configuration.GetSection(OpenApiOption.ConfigurationKey).Get<OpenApiOption>() ?? new OpenApiOption();

        // Add Swagger for FastEndpoints
        services.SwaggerDocument(o => {
            o.DocumentSettings = s => {
                s.Title = options.Title;
                s.Description = options.Description;
                s.Version = options.Version;
            };
            // Specify tags for organizing endpoints
            // o.TagsSelector = endpoint => new[] { endpoint.HttpMethod };
        });

        return services;
    }

    public static WebApplication UseOpenApiConfiguration(this WebApplication app, IConfiguration configuration)
    {
        // Only expose Swagger in Development/Staging or if explicitly enabled
        // Fix: Get the setting from OpenApi section instead of non-existent ApiSettings
        var openApiSettings = configuration.GetSection(OpenApiOption.ConfigurationKey);
        var enableSwagger = app.Environment.IsDevelopment() ||
                            app.Environment.IsStaging() ||
                            openApiSettings.GetValue<bool>("EnableSwaggerInProduction");

        if (enableSwagger)
        {
            // Configure Swagger middleware
            app.UseSwaggerGen();

            // Configure Swagger UI with explicit endpoint
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RocketOps API v1");
                c.RoutePrefix = "swagger";
                c.DefaultModelsExpandDepth(-1); // Hide schemas section by default
                c.DocExpansion(DocExpansion.List);
            });
        }

        return app;
    }
}