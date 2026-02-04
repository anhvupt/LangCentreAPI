using LangCentre.Domain.Events;

namespace LangCentre.Infra.Auditing;

internal sealed class NoOpDomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default) =>
        Task.CompletedTask;
}
