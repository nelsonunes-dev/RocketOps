namespace RocketOps.Core.Application.CQRS;

public interface ICqrsCommand
{
    Guid Id { get; }
}
