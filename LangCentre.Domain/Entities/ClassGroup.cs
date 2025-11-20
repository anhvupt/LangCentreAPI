namespace LangCentre.Domain.Entities;

public class ClassGroup: BaseEntity<Guid>
{
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public ICollection<Class> Classes { get; } = [];
}