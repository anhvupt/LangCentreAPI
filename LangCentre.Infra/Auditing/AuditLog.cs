namespace LangCentre.Infra.Auditing;

public class AuditLog
{
    public Guid Id { get; set; }
    public string EventType { get; set; } = default!;
    public string AggregateId { get; set; } = default!;
    public DateTime Timestamp { get; set; }
    public string? Payload { get; set; }
}
