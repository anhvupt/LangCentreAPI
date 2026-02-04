using LangCentre.Domain.Enums;

namespace LangCentre.Domain.Events;

public record CourseUpdated(
    Guid CourseId,
    string Name,
    LangLevel Level,
    DateTime OccurredAt) : IDomainEvent;
