using System.ComponentModel.DataAnnotations.Schema;
using LangCentre.Domain.Events;

namespace LangCentre.Domain.Entities;

public abstract class BaseEntity<TId> : IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [NotMapped]
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}