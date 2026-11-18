using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Entities;
using AgenticCommerce.Core.Dtos;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AgenticCommerce.Infra.Repository;

public class ProductRepository(AgenticCommerceDbContext context) : IProductRepository
{
    public Task<int> CountAsync(ProductSearchCriteria criteria, CancellationToken cancellationToken = default)
        => ApplyFilters(criteria).CountAsync(cancellationToken);

    public async Task<IReadOnlyList<Product>> GetPageAsync(ProductSearchCriteria criteria, int skip, int take, CancellationToken cancellationToken = default)
        => await ApplyFilters(criteria).OrderBy(product => product.Id)
            .Skip(skip).Take(take).ToListAsync(cancellationToken);

    private IQueryable<Product> ApplyFilters(ProductSearchCriteria criteria)
    {
        var query = context.Products.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(criteria.SearchTerm))
            query = query.Where(product => product.Name.Contains(criteria.SearchTerm) ||
                product.Description.Contains(criteria.SearchTerm));
        if (criteria.MinPrice.HasValue)
            query = query.Where(product => product.Price >= criteria.MinPrice.Value);
        if (criteria.MaxPrice.HasValue)
            query = query.Where(product => product.Price <= criteria.MaxPrice.Value);
        if (criteria.InStockOnly)
            query = query.Where(product => product.StockQuantity > 0);
        return query;
    }

    public void Add(Product entity) => throw new NotImplementedException();
    public void Remove(Product entity) => throw new NotImplementedException();
    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => context.Products.SingleOrDefaultAsync(product => product.Id == id, cancellationToken);
    public Task<Product?> FindByAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}
