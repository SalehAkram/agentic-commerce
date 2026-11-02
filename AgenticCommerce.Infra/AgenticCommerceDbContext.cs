using AgenticCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgenticCommerce.Infra;

public class AgenticCommerceDbContext(DbContextOptions<AgenticCommerceDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}
