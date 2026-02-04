using LangCentre.Domain.Enums;

namespace LangCentre.Domain.Events;

public record CourseCreated(
    Guid CourseId,
    string Name,
    LangLevel Level,
    DateTime OccurredAt) : IDomainEvent;
