namespace LibraryManagement.API.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow; // Define um valor padrão para data de criação
    }

    protected BaseEntity(int id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
