using AgenticCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgenticCommerce.Infra.Seeding;

public static class ProductSeeder
{
    public static async Task SeedAsync(AgenticCommerceDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Products.AnyAsync(cancellationToken))
        {
            return;
        }

        context.Products.AddRange(
            new Product("Everyday Trainers", "Lightweight trainers for everyday wear.", 59.99m, 20),
            new Product("Running Shoes", "Cushioned shoes for road running.", 109.99m, 12),
            new Product("Cotton T-Shirt", "A breathable cotton crew-neck T-shirt.", 19.99m, 50),
            new Product("Sports Backpack", "A compact backpack for gym essentials.", 34.50m, 8),
            new Product("Waterproof Jacket", "A lightweight jacket for wet weather.", 89.00m, 0));

        await context.SaveChangesAsync(cancellationToken);
    }
}
