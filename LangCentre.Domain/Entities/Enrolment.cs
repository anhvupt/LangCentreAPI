namespace LangCentre.Domain.Entities;

public class Enrolment : BaseEntity<Guid>
{
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid ClassId { get; set; }
    public Class? Class { get; set; }

    public DateTime EnrolledDate { get; set; }

    public static Enrolment Create(Guid studentId, Guid classId)
    {
        var now = DateTime.UtcNow;
        var enrolment = new Enrolment
        {
            Id = Guid.NewGuid(),
            StudentId = studentId,
            ClassId = classId,
            EnrolledDate = now,
            CreatedAt = now,
            UpdatedAt = now
        };
        enrolment.AddDomainEvent(new Events.EnrolmentCreated(enrolment.Id, enrolment.StudentId, enrolment.ClassId, enrolment.EnrolledDate, now));
        enrolment.AddDomainEvent(new Events.StudentEnrolled(enrolment.StudentId, enrolment.ClassId, enrolment.Id, enrolment.EnrolledDate, now));
        return enrolment;
    }
}