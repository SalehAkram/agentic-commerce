namespace AgenticCommerce.Core.Contracts;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    IBasketRepository BasketRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
