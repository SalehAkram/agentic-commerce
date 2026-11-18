using AgenticCommerce.Core.Shared.Contracts;

namespace AgenticCommerce.Infra;

public sealed class UnitOfWork(AgenticCommerceDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
