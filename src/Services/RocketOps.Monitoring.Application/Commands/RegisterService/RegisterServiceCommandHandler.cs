using Microsoft.Extensions.Logging;
using RocketOps.Core.Application.CQRS;
using RocketOps.Monitoring.Domain.Entities;
using RocketOps.Monitoring.Infrastructure.Repositories.Abstractions;

namespace RocketOps.Monitoring.Application.Commands.RegisterService;

public class RegisterServiceCommandHandler : ICqrsCommandHandler<RegisterServiceCommand>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ILogger<RegisterServiceCommandHandler> _logger;

    public RegisterServiceCommandHandler(
        IServiceRepository serviceRepository,
        ILogger<RegisterServiceCommandHandler> logger)
    {
        _serviceRepository = serviceRepository;
        _logger = logger;
    }

    public async Task HandleAsync(
        RegisterServiceCommand command,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Registering new service: {Name}", command.Name);

        var service = new Service(
            command.Name,
            command.BaseUrl,
            command.Description);

        await _serviceRepository.AddAsync(service);

        _logger.LogInformation("Service registered with ID: {Id}", service.Id);
    }
}
