using AgenticCommerce.Core.Entities;

namespace AgenticCommerce.Core.Contracts;

public interface IBasketRepository : IRepository<Basket, int>
{
    Task<Basket?> GetActiveByCustomerIdAsync(
        Guid customerId, CancellationToken cancellationToken = default);
}
