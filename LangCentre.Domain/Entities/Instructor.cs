using LangCentre.Domain.Enums;

namespace LangCentre.Domain.Entities;

public class Instructor: BaseEntity<Guid>
{
    public string Name { get; set; }
    public LangLevel Level { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public ICollection<Course> Courses { get; } = [];
    public ICollection<Class> Classes { get; } = [];
}