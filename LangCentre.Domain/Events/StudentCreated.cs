using LangCentre.Domain.Enums;

namespace LangCentre.Domain.Events;

public record StudentCreated(
    Guid StudentId,
    string Name,
    string Address,
    DateTime DateOfBirth,
    LangLevel PreferredLevel,
    DateTime OccurredAt) : IDomainEvent;
