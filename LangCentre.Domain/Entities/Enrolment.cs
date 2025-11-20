namespace LangCentre.Domain.Entities;

public class Enrolment: BaseEntity<Guid>
{
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid ClassId { get; set; }
    public Class? Class { get; set; }
    
    public DateTime EnrolledDate { get; set; }
}