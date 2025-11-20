using LangCentre.Domain.Enums;

namespace LangCentre.Domain.Entities;

public class Student: BaseEntity<Guid>
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public LangLevel PreferredLevel { get; set; }

    public ICollection<Enrolment> Enrolments { get; } = [];
}