namespace LangCentre.Domain.Entities;

public class BaseEntity<TId>
{
    public TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}