using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using RocketOps.Core.Application.CQRS;
using RocketOps.Monitoring.Application.Commands.CreateAlert;
using RocketOps.Monitoring.Application.Commands.RegisterService;
using RocketOps.Monitoring.Application.Queries.GetAlerts;

namespace RocketOps.Monitoring.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMonitoringApplicationServices(this IServiceCollection services)
    {
        // Register FastEndpoints
        services.AddFastEndpoints();

        // Register command handlers
        services.AddScoped<ICqrsCommandHandler<CreateAlertCommand>, CreateAlertCommandHandler>();
        services.AddScoped<ICqrsCommandHandler<RegisterServiceCommand>, RegisterServiceCommandHandler>();

        // Register query handlers - only register what we've properly implemented
        services.AddScoped<ICqrsQueryHandler<GetAlertsQuery, IEnumerable<AlertDto>>, GetAlertsQueryHandler>();

        return services;
    }
}
