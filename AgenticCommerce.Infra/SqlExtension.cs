using Microsoft.EntityFrameworkCore;
using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Infra.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgenticCommerce.Infra;

public static class SqlExtension
{
    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["Sql:ConnectionString"];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Configuration 'Sql:ConnectionString' is required.");
        }

        services.AddDbContext<AgenticCommerceDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
