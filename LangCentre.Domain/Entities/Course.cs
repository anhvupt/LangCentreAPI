using LangCentre.Domain.Enums;

namespace LangCentre.Domain.Entities;

public class Course : BaseEntity<Guid>
{
    public string Name { get; set; } = default!;
    public LangLevel Level { get; set; }

    public static Course Create(string name, LangLevel level)
    {
        var now = DateTime.UtcNow;
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = name,
            Level = level,
            CreatedAt = now,
            UpdatedAt = now
        };
        course.AddDomainEvent(new Events.CourseCreated(course.Id, course.Name, course.Level, now));
        return course;
    }
}