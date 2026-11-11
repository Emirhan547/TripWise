namespace TripWise.Domain.Abstractions;

public abstract class Entity
{
    
    public Guid Id { get; protected set; }= Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public void Delete()
    {
        if (IsDeleted)
            return;
        IsDeleted = true;
        DeletedAt = DateTimeOffset.Now;
    }

}
