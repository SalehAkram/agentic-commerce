using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Entities;
using System.Linq.Expressions;

namespace AgenticCommerce.Infra.Repository;

public class ProductRepository : IProductRepository
{
    public void Add(Product entity) => throw new NotImplementedException();
    public void Remove(Product entity) => throw new NotImplementedException();
    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
    public Task<Product?> FindByAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}
