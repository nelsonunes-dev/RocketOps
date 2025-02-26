using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RocketOps.Core.Infrastructure.Configurations;

public static class FastEndpointsConfiguration
{
    public static IServiceCollection AddFastEndpointsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Register FastEndpoints
        services.AddFastEndpoints();

        // Add CORS configuration
        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCorsPolicy", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        // Add JWT Auth if configured
        var jwtSettings = configuration.GetSection("JwtSettings");
        if (jwtSettings.Exists())
        {
            var secretKey = jwtSettings["SecretKey"];
            if (!string.IsNullOrEmpty(secretKey))
            {
                services.AddJWTBearerAuth(secretKey);

                services.AddAuthorization(options =>
                {
                    options.AddPolicy("AdminOnly", policy =>
                    {
                        policy.RequireRole("Admin");
                    });

                    options.AddPolicy("UserAccess", policy =>
                    {
                        policy.RequireAuthenticatedUser();
                    });
                });
            }
        }

        return services;
    }

    public static WebApplication UseFastEndpointsConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        // Configure FastEndpoints middleware
        app.UseFastEndpoints(config =>
        {
            // Configure global endpoints settings
            config.Endpoints.RoutePrefix = "api";

            // Configure error handling
            config.Errors.ResponseBuilder = (failures, ctx, statusCode) =>
            {
                return new ValidationFailureResponse
                {
                    StatusCode = statusCode,
                    Errors = failures.Select(f => new ValidationError
                    {
                        PropertyName = f.PropertyName,
                        ErrorMessage = f.ErrorMessage,
                        ErrorCode = f.ErrorCode,
                        Severity = f.Severity.ToString()
                    }).ToList()
                };
            };
        });

        // Use CORS
        app.UseCors("DefaultCorsPolicy");

        // Configure auth
        if (env.IsProduction())
        {
            app.UseHttpsRedirection();
        }

        // Uncomment these lines if your application requires authentication
        var jwtSettings = app.Configuration.GetSection("JwtSettings");
        if (jwtSettings.Exists() && !string.IsNullOrEmpty(jwtSettings["SecretKey"]))
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        return app;
    }
}

public class ValidationFailureResponse
{
    public int StatusCode { get; set; }
    public List<ValidationError> Errors { get; set; } = new();
}

public class ValidationError
{
    public string PropertyName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
}