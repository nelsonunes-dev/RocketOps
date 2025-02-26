namespace RocketOps.Core.Application.CQRS;

public interface ICqrsCommandHandler<TCommand> where TCommand : ICqrsCommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
