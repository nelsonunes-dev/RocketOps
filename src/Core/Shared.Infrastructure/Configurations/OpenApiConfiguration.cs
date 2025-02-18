namespace Shared.Infrastructure.Configurations;

internal static class OpenApiConfiguration
{
    internal static IServiceCollection AddOpenApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection("ApiSettings");
        var title = apiSettings["Title"] ?? "RocketOps API";
        var description = apiSettings["Description"] ?? "RocketOps API Documentation";
        var version = apiSettings["Version"] ?? "v1";

        // Add Swagger for FastEndpoints 5.34.0
        services.SwaggerDocument();

        return services;
    }

    internal static WebApplication UseOpenApiConfiguration(this WebApplication app, IConfiguration configuration)
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