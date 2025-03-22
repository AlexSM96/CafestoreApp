namespace Cafestore.Domain.Entities.Base;

public abstract class BaseEntity
{
    public required long Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public required bool IsDeleted { get; set; }
}
