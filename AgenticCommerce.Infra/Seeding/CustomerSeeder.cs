using AgenticCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgenticCommerce.Infra.Seeding;

public static class CustomerSeeder
{
    public const string DevelopmentCustomerEmail = "customer@example.com";

    public static async Task SeedAsync(AgenticCommerceDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Customers.AnyAsync(customer => customer.Email == DevelopmentCustomerEmail, cancellationToken))
            return;

        context.Customers.Add(new Customer("Development", "Customer", DevelopmentCustomerEmail));
        await context.SaveChangesAsync(cancellationToken);
    }
}
