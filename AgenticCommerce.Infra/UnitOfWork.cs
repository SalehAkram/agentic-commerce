using AgenticCommerce.Core.Contracts;

namespace AgenticCommerce.Infra;

public sealed class UnitOfWork(AgenticCommerceDbContext context,
    IProductRepository productRepository, IBasketRepository basketRepository, ICustomerRepository customerRepository) : IUnitOfWork
{
    public IProductRepository ProductRepository { get; } = productRepository;
    public IBasketRepository BasketRepository { get; } = basketRepository;
    public ICustomerRepository CustomerRepository { get; } = customerRepository;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
