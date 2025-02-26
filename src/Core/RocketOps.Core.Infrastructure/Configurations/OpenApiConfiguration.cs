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
        var options = configuration.GetSection(OpenApiOption.ConfigurationKey).Get<OpenApiOption>()
            ?? new OpenApiOption();

        // Add Swagger for FastEndpoints
        services.SwaggerDocument(o => {
            o.DocumentSettings = s => {
                s.Title = options.Title;
                s.Description = options.Description;
                s.Version = options.Version;
            };
        });

        return services;
    }

    public static WebApplication UseOpenApiConfiguration(this WebApplication app, IConfiguration configuration)
    {
        // Only expose Swagger in Development/Staging or if explicitly enabled
        var apiSettings = configuration.GetSection("ApiSettings");
        var enableSwagger = app.Environment.IsDevelopment() || app.Environment.IsStaging() || apiSettings.GetValue<bool>("EnableSwaggerInProduction");

        if (enableSwagger)
        {
            // Configure Swagger middleware
            app.UseSwaggerGen();

            // Configure Swagger UI
            var routePrefix = apiSettings["SwaggerRoute"] ?? "swagger";
            app.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1); // Hide schemas section by default
                options.DocExpansion(DocExpansion.List);
                options.RoutePrefix = routePrefix;
                options.DocumentTitle = apiSettings["Title"] ?? "RocketOps API Documentation";

                // Add custom CSS/JS if configured
                var customCss = apiSettings["SwaggerCustomCss"];
                if (!string.IsNullOrEmpty(customCss))
                {
                    options.InjectStylesheet(customCss);
                }

                var customJs = apiSettings["SwaggerCustomJs"];
                if (!string.IsNullOrEmpty(customJs))
                {
                    options.InjectJavascript(customJs);
                }
            });
        }

        return app;
    }
}

public class ApiServer
{
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}