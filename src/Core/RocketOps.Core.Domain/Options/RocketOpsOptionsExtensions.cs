using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RocketOps.Core.Domain.Options;

public static class RocketOpsOptionsExtensions
{
    public static T LoadOptions<T>(this IConfiguration configuration) where T : IValidateRocketOpsOptions, new()
    {
        var options = new T();
        var sectionName = GetSectionName<T>();
        return configuration.GetSection(sectionName).Get<T>() ?? options;
    }

    public static T LoadOptions<T>(this IConfiguration configuration, string sectionName) where T : IValidateRocketOpsOptions, new()
    {
        var options = new T();
        return configuration.GetSection(sectionName).Get<T>() ?? options;
    }

    public static T BindValidateReturn<T>(this IServiceCollection services, IConfiguration configuration) where T : class, IValidateRocketOpsOptions, new()
    {
        var options = new T();
        var sectionName = GetSectionName<T>();

        services.AddOptions<T>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return configuration.LoadOptions<T>();
    }

    public static void BindValidate<T>(this IServiceCollection services) where T : class, IValidateRocketOpsOptions, new()
    {
        var options = new T();
        var sectionName = GetSectionName<T>();

        services.AddOptions<T>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static string GetSectionName<T>()
    {
        return typeof(T).GetField("ConfigurationKey")?.GetValue(null)?.ToString() ?? typeof(T).Name;
    }
}
