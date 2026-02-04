using LangCentre.Domain.Enums;

namespace LangCentre.Domain.Entities;

public class Student : BaseEntity<Guid>
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public LangLevel PreferredLevel { get; set; }

    public ICollection<Enrolment> Enrolments { get; } = [];

    public static Student Create(string name, string address, DateTime dateOfBirth, LangLevel preferredLevel)
    {
        var now = DateTime.UtcNow;
        var student = new Student
        {
            Id = Guid.NewGuid(),
            Name = name,
            Address = address,
            DateOfBirth = dateOfBirth,
            PreferredLevel = preferredLevel,
            CreatedAt = now,
            UpdatedAt = now
        };
        student.AddDomainEvent(new Events.StudentCreated(student.Id, student.Name, student.Address, student.DateOfBirth, student.PreferredLevel, now));
        return student;
    }
}