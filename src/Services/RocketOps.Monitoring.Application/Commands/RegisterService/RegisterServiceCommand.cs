using RocketOps.Core.Application.CQRS;

namespace RocketOps.Monitoring.Application.Commands.RegisterService;

public class RegisterServiceCommand : ICqrsCommand
{
    public Guid Id { get; }
    public string Name { get; }
    public string BaseUrl { get; }
    public string Description { get; }

    public RegisterServiceCommand(string name, string baseUrl, string description = "")
    {
        Id = Guid.NewGuid();
        Name = name;
        BaseUrl = baseUrl;
        Description = description;
    }
}
