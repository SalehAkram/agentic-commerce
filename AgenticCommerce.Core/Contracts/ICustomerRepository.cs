using AgenticCommerce.Core.Entities;

namespace AgenticCommerce.Core.Contracts;

public interface ICustomerRepository: IRepository<Customer, Guid>
{
}
