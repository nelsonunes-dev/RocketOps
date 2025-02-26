namespace RocketOps.Core.Application.CQRS;

public interface ICqrsQueryHandler<TQuery, TResult> where TQuery : ICqrsQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
