using System.Text.Json;
using LangCentre.Domain.Events;
using LangCentre.Infra.Persistent;

namespace LangCentre.Infra.Auditing;

public class AuditDomainEventHandler(LangCentreDbContext dbContext) : IDomainEventDispatcher
{
    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default)
    {
        foreach (var evt in domainEvents)
        {
            var auditEntry = new AuditLog
            {
                Id = Guid.NewGuid(),
                EventType = evt.GetType().Name,
                AggregateId = GetAggregateId(evt),
                Timestamp = DateTime.UtcNow,
                Payload = JsonSerializer.Serialize(evt)
            };

            dbContext.Set<AuditLog>().Add(auditEntry);
        }

        return Task.CompletedTask;
    }

    private static string GetAggregateId(IDomainEvent evt)
    {
        return evt switch
        {
            LangCentre.Domain.Events.CourseCreated e => e.CourseId.ToString(),
            LangCentre.Domain.Events.CourseUpdated e => e.CourseId.ToString(),
            LangCentre.Domain.Events.StudentCreated e => e.StudentId.ToString(),
            LangCentre.Domain.Events.StudentEnrolled e => e.EnrolmentId.ToString(),
            LangCentre.Domain.Events.EnrolmentCreated e => e.EnrolmentId.ToString(),
            _ => Guid.Empty.ToString()
        };
    }
}
