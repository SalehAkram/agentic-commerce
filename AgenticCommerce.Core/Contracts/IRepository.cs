using System.Linq.Expressions;

namespace AgenticCommerce.Core.Contracts;

public interface IRepository<TEntity, TId> where TEntity : IAggregateRoot
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<TEntity?> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}
