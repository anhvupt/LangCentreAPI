namespace LangCentre.Domain.Entities;

public class Class: BaseEntity<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public Guid InstructorId { get; set; }
    public Instructor? Instructor { get; set; }
    public Guid ClassGroupId { get; set; }
    public ClassGroup? ClassGroup { get; set; }
    
    public ICollection<Enrolment> Enrolments { get; } = [];
}