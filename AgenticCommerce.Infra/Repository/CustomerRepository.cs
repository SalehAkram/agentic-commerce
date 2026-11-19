using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgenticCommerce.Infra.Repository;

public class CustomerRepository(AgenticCommerceDbContext context) : ICustomerRepository
{
    public void Add(Customer entity)
    {
        context.Customers.Add(entity);
    }
    public void Remove(Customer entity)
    {
        context.Customers.Remove(entity);
    }
    public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Customers.SingleOrDefaultAsync(customer => customer.Id == id, cancellationToken);
    }
    public Task<Customer?> FindByAsync(Expression<Func<Customer, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return context.Customers.SingleOrDefaultAsync(predicate, cancellationToken);
    }
}
