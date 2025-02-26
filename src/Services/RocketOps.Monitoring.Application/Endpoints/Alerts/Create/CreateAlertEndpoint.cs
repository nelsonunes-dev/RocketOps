using FastEndpoints;
using RocketOps.Core.Application.CQRS;
using RocketOps.Monitoring.Application.Commands.CreateAlert;
using RocketOps.Monitoring.Application.Endpoints.Alerts.Get;

namespace RocketOps.Monitoring.Application.Endpoints.Alerts.Create;

public class CreateAlertEndpoint : Endpoint<CreateAlertRequest, CreateAlertResponse>
{
    private readonly ICqrsCommandHandler<CreateAlertCommand> _commandHandler;

    public CreateAlertEndpoint(ICqrsCommandHandler<CreateAlertCommand> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public override void Configure()
    {
        Post("/api/alerts");
        AllowAnonymous(); // Replace with proper auth
        Summary(s =>
        {
            s.Summary = "Create a new alert";
            s.Description = "Creates a new alert in the system";
            //s.ResponseDescription = "Returns the created alert ID";
            //s.Tags = new[] { "Alerts" };
        });
    }

    public override async Task HandleAsync(CreateAlertRequest req, CancellationToken ct)
    {
        // Updated to include ServiceName parameter
        var command = new CreateAlertCommand(req.Name, req.Description, req.Severity, req.ServiceName);
        await _commandHandler.HandleAsync(command, ct);

        var response = new CreateAlertResponse
        {
            Id = command.Id
        };

        await SendCreatedAtAsync<GetAlertsEndpoint>(
            new { id = command.Id },
            response,
            generateAbsoluteUrl: true,
            cancellation: ct);
    }
}
