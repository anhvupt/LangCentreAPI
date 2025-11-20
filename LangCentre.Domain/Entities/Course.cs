using LangCentre.Domain.Enums;

namespace LangCentre.Domain.Entities;

public class Course: BaseEntity<Guid>
{
    public string Name { get; set; }
    public LangLevel Level { get; set; }
}