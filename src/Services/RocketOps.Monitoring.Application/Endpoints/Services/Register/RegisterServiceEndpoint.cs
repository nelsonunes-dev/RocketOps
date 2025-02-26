// Place this file in src/Services/RocketOps.Monitoring.Application/Endpoints/Services/RegisterServiceEndpoint.cs
using FastEndpoints;
using RocketOps.Core.Application.CQRS;
using RocketOps.Monitoring.Application.Commands.RegisterService;

namespace RocketOps.Monitoring.Application.Endpoints.Services.Register;

public class RegisterServiceEndpoint : Endpoint<RegisterServiceRequest, RegisterServiceResponse>
{
    private readonly ICqrsCommandHandler<RegisterServiceCommand> _commandHandler;

    public RegisterServiceEndpoint(ICqrsCommandHandler<RegisterServiceCommand> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public override void Configure()
    {
        Post("/api/services");
        AllowAnonymous(); // Replace with proper auth
        Summary(s =>
        {
            s.Summary = "Register a new service";
            s.Description = "Registers a new service for monitoring";
        });
    }

    public override async Task HandleAsync(RegisterServiceRequest req, CancellationToken ct)
    {
        var command = new RegisterServiceCommand(req.Name, req.BaseUrl, req.Description);
        await _commandHandler.HandleAsync(command, ct);

        var response = new RegisterServiceResponse
        {
            Id = command.Id,
            Name = req.Name,
            Message = "Service registered successfully"
        };

        await SendAsync(response, 201, cancellation: ct);
    }
}
