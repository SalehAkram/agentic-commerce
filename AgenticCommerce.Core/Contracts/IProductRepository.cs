using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Entities;
using AgenticCommerce.Core.Dtos;

namespace AgenticCommerce.Core.Contracts;

public interface IProductRepository : IRepository<Product, int>
{
    Task<IReadOnlyList<Product>> GetByIdsAsync(
        IReadOnlyCollection<int> ids, CancellationToken cancellationToken = default);

    Task<int> CountAsync(ProductSearchCriteria criteria, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetPageAsync(ProductSearchCriteria criteria, int skip, int take, CancellationToken cancellationToken = default);
}
