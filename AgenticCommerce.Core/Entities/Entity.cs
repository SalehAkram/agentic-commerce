namespace AgenticCommerce.Core.Entities;

public abstract class Entity<TId> where TId : notnull
{
    public TId Id { get; protected set; } = default!;
}
