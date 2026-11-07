using AgenticCommerce.Core.Entities;
using AgenticCommerce.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AgenticCommerce.Infra;

public class AgenticCommerceDbContext(DbContextOptions<AgenticCommerceDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
