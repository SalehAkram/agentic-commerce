using AgenticCommerce.Core.Modules.Catalogue.Contracts;
using Microsoft.EntityFrameworkCore;
using AgenticCommerce.Core.Shared.Contracts;
using AgenticCommerce.Infra.Modules.Catalogue.Repository;
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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
