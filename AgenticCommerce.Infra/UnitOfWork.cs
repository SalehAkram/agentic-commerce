using AgenticCommerce.Core.Contracts;

namespace AgenticCommerce.Infra;

public sealed class UnitOfWork(AgenticCommerceDbContext context, IProductRepository productRepository) : IUnitOfWork
{
    public IProductRepository ProductRepository { get; } = productRepository;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
