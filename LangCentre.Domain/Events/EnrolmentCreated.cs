namespace LangCentre.Domain.Events;

public record EnrolmentCreated(
    Guid EnrolmentId,
    Guid StudentId,
    Guid ClassId,
    DateTime EnrolledDate,
    DateTime OccurredAt) : IDomainEvent;
