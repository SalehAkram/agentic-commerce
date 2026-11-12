using AgenticCommerce.Core.Entities;

namespace AgenticCommerce.Core.Contracts;

public interface IProductRepository : IRepository<Product, int>
{
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetPageAsync(int skip, int take, CancellationToken cancellationToken = default);
}
