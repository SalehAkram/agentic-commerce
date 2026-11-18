using AgenticCommerce.Core.Modules.Catalogue.Contracts;
using AgenticCommerce.Core.Shared.Contracts;
using AgenticCommerce.Core.Modules.Catalogue.Queries;
using AgenticCommerce.Core.Modules.Catalogue.Dtos;
using MediatR;

namespace AgenticCommerce.Core.Modules.Catalogue.QueryHandlers;

public sealed class GetProductsQueryHandler(IProductRepository repository)
    : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var criteria = new ProductSearchCriteria(request.SearchTerm?.Trim(),
            request.MinPrice, request.MaxPrice, request.InStockOnly);
        var skip = (int)(((long)request.PageNumber - 1) * request.PageSize);
        var totalCount = await repository.CountAsync(criteria, cancellationToken);
        var products = await repository.GetPageAsync(criteria, skip, request.PageSize, cancellationToken);
        var items = products.Select(product => new ProductSummary(
            product.Id, product.Name, product.Description, product.Price, product.StockQuantity)).ToArray();

        return new GetProductsResult(items, request.PageNumber, request.PageSize, totalCount);
    }
}

