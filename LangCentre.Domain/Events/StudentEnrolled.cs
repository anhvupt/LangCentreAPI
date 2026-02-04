namespace LangCentre.Domain.Events;

public record StudentEnrolled(
    Guid StudentId,
    Guid ClassId,
    Guid EnrolmentId,
    DateTime EnrolledDate,
    DateTime OccurredAt) : IDomainEvent;
