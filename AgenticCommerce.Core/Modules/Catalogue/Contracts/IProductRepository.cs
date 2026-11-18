using AgenticCommerce.Core.Shared.Contracts;
using AgenticCommerce.Core.Modules.Catalogue.Entities;
using AgenticCommerce.Core.Modules.Catalogue.Dtos;

namespace AgenticCommerce.Core.Modules.Catalogue.Contracts;

public interface IProductRepository : IRepository<Product, int>
{
    Task<int> CountAsync(ProductSearchCriteria criteria, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetPageAsync(ProductSearchCriteria criteria, int skip, int take, CancellationToken cancellationToken = default);
}
