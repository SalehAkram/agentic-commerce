using System.Linq.Expressions;
using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgenticCommerce.Infra.Repository;

public sealed class BasketRepository(AgenticCommerceDbContext context) : IBasketRepository
{
    public void Add(Basket entity)
    {
        context.Baskets.Add(entity);
    }

    public void Remove(Basket entity)
    {
        context.Baskets.Remove(entity);
    }

    public Task<Basket?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Baskets.Include(basket => basket.Items).SingleOrDefaultAsync(basket => basket.Id == id, cancellationToken);
    }

    public Task<Basket?> FindByAsync(Expression<Func<Basket, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return context.Baskets.Include(basket => basket.Items).SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<Basket?> GetActiveByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return context.Baskets.Include(basket => basket.Items).SingleOrDefaultAsync(basket => basket.CustomerId == customerId && basket.Status == BasketStatus.Active, cancellationToken);
    }
}
