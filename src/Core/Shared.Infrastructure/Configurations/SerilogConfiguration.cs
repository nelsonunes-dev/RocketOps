namespace Shared.Infrastructure.Configurations;

public static class SerilogConfiguration
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        // Get log settings from configuration
        var logSettings = configuration.GetSection("LogSettings");
        var applicationName = logSettings["ApplicationName"] ?? "RocketOps";
        var minimumLevel = GetLogEventLevel(logSettings["MinimumLevel"] ?? "Information");
        var outputTemplate = logSettings["OutputTemplate"] ?? "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

        // Configure basic Serilog logger
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(minimumLevel)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console(outputTemplate: outputTemplate)
            .CreateLogger();

        // Configure logging
        return hostBuilder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddSerilog(dispose: true);
        });
    }

    public static IServiceCollection AddSerilogConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Get log settings from configuration
        var logSettings = configuration.GetSection("LogSettings");
        var applicationName = logSettings["ApplicationName"] ?? "RocketOps";
        var minimumLevel = GetLogEventLevel(logSettings["MinimumLevel"] ?? "Information");
        var outputTemplate = logSettings["OutputTemplate"] ?? "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        var logPath = GetLogFilePath(logSettings);
        var retainedFileCountLimit = int.TryParse(logSettings["RetainedFileCountLimit"], out var limit) ? limit : 31;
        var fileSizeLimitBytes = long.TryParse(logSettings["FileSizeLimitBytes"], out var size) ? size : 10485760;

        // Configure Serilog
        var loggerConfiguration = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithProperty("Application", applicationName)
            .Enrich.WithExceptionDetails();

        // Configure minimum level
        loggerConfiguration.MinimumLevel.Is(minimumLevel);

        // Configure level overrides
        ConfigureLevelOverrides(loggerConfiguration, configuration);

        // Add console logging for all environments
        if (logSettings.GetValue<bool>("EnableConsoleLogging", true))
        {
            if (logSettings.GetValue<bool>("UseCompactJsonConsoleLogging", false))
            {
                loggerConfiguration.WriteTo.Console(new CompactJsonFormatter());
            }
            else
            {
                loggerConfiguration.WriteTo.Console(outputTemplate: outputTemplate);
            }
        }

        // Add file logging if configured
        if (logSettings.GetValue<bool>("EnableFileLogging", true) && !string.IsNullOrEmpty(logPath))
        {
            if (logSettings.GetValue<bool>("UseCompactJsonFileLogging", false))
            {
                loggerConfiguration.WriteTo.File(
                    new CompactJsonFormatter(),
                    logPath,
                    rollingInterval: GetRollingInterval(logSettings["RollingInterval"]),
                    retainedFileCountLimit: retainedFileCountLimit,
                    fileSizeLimitBytes: fileSizeLimitBytes);
            }
            else
            {
                loggerConfiguration.WriteTo.File(
                    logPath,
                    outputTemplate: outputTemplate,
                    rollingInterval: GetRollingInterval(logSettings["RollingInterval"]),
                    retainedFileCountLimit: retainedFileCountLimit,
                    fileSizeLimitBytes: fileSizeLimitBytes);
            }
        }

        // Set the logger
        Log.Logger = loggerConfiguration.CreateLogger();

        return services;
    }

    private static string GetLogFilePath(IConfiguration logSettings)
    {
        var logDirectory = logSettings["LogDirectory"] ?? "Logs";
        var logFileName = logSettings["LogFileName"] ?? "rocketops-.log";

        // Ensure directory exists
        if (!Path.IsPathRooted(logDirectory))
        {
            logDirectory = Path.Combine(AppContext.BaseDirectory, logDirectory);
        }

        Directory.CreateDirectory(logDirectory);
        return Path.Combine(logDirectory, logFileName);
    }

    private static LogEventLevel GetLogEventLevel(string levelName)
    {
        return levelName?.ToLower() switch
        {
            "verbose" => LogEventLevel.Verbose,
            "debug" => LogEventLevel.Debug,
            "information" => LogEventLevel.Information,
            "warning" => LogEventLevel.Warning,
            "error" => LogEventLevel.Error,
            "fatal" => LogEventLevel.Fatal,
            _ => LogEventLevel.Information
        };
    }

    private static RollingInterval GetRollingInterval(string interval)
    {
        return interval?.ToLower() switch
        {
            "minute" => RollingInterval.Minute,
            "hour" => RollingInterval.Hour,
            "day" => RollingInterval.Day,
            "month" => RollingInterval.Month,
            "infinite" => RollingInterval.Infinite,
            _ => RollingInterval.Day
        };
    }

    private static void ConfigureLevelOverrides(LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        var overrides = configuration.GetSection("LogSettings:LevelOverrides").GetChildren();

        foreach (var @override in overrides)
        {
            var namespacePrefix = @override.Key;
            var level = GetLogEventLevel(@override.Value);
            loggerConfiguration.MinimumLevel.Override(namespacePrefix, level);
        }

        // Common overrides
        loggerConfiguration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
    }

    public static IApplicationBuilder UseSerilogRequestLogging(this IApplicationBuilder app)
    {
        // Custom middleware for request logging
        return app.Use(async (context, next) =>
        {
            var start = DateTime.UtcNow;

            try
            {
                await next();
                var elapsed = (DateTime.UtcNow - start).TotalMilliseconds;

                var level = LogEventLevel.Information;
                if (context.Response.StatusCode >= 500 || elapsed > 5000)
                    level = LogEventLevel.Error;
                else if (context.Response.StatusCode >= 400 || elapsed > 1000)
                    level = LogEventLevel.Warning;

                Log.Write(level,
                    "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    elapsed);
            }
            catch (Exception ex)
            {
                var elapsed = (DateTime.UtcNow - start).TotalMilliseconds;
                Log.Error(ex,
                    "HTTP {RequestMethod} {RequestPath} failed in {Elapsed:0.0000} ms",
                    context.Request.Method,
                    context.Request.Path,
                    elapsed);
                throw;
            }
        });
    }
}