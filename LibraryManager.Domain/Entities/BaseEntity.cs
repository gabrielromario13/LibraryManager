namespace LibraryManager.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; private set; } = true;

    public void SetId(int id) => Id = id;

    public void Deactivate()
    {
        IsActive = false;
    }
}