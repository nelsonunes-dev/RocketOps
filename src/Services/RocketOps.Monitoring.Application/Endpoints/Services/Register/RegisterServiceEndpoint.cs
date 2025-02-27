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
        // Remove the "api/" prefix since it's already configured globally
        Post("/services");
        AllowAnonymous(); // Replace with proper auth
        Summary(s =>
        {
            s.Summary = "Register a new service";
            s.Description = "Registers a new service for monitoring";
            s.ExampleRequest = new RegisterServiceRequest
            {
                Name = "Example Service",
                BaseUrl = "https://example.com/api",
                Description = "An example service for monitoring"
            };
            s.Response<RegisterServiceResponse>(201, "Service created successfully");
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
