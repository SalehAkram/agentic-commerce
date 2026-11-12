using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Queries;
using AgenticCommerce.Core.Results;
using MediatR;

namespace AgenticCommerce.Core.QueryHandlers;

public sealed class GetProductsQueryHandler(IProductRepository repository)
    : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(request.PageNumber, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(request.PageSize, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(request.PageSize, 100);
        var offset = ((long)request.PageNumber - 1) * request.PageSize;
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, int.MaxValue);

        var totalCount = await repository.CountAsync(cancellationToken);
        var products = await repository.GetPageAsync((int)offset, request.PageSize, cancellationToken);
        var items = products.Select(product => new ProductSummary(
            product.Id, product.Name, product.Description, product.Price, product.StockQuantity)).ToArray();

        return new GetProductsResult(items, request.PageNumber, request.PageSize, totalCount);
    }
}
