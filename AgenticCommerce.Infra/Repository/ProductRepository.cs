using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AgenticCommerce.Infra.Repository;

public class ProductRepository(AgenticCommerceDbContext context) : IProductRepository
{
    public Task<int> CountAsync(CancellationToken cancellationToken = default)
        => context.Products.CountAsync(cancellationToken);

    public async Task<IReadOnlyList<Product>> GetPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        => await context.Products.AsNoTracking().OrderBy(product => product.Id)
            .Skip(skip).Take(take).ToListAsync(cancellationToken);

    public void Add(Product entity) => throw new NotImplementedException();
    public void Remove(Product entity) => throw new NotImplementedException();
    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
    public Task<Product?> FindByAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}
