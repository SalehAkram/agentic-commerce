using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Dtos;
using AgenticCommerce.Core.Exceptions;
using AgenticCommerce.Core.Queries;
using MediatR;

namespace AgenticCommerce.Core.QueryHandlers;

public sealed class GetProductByIdQueryHandler(IProductRepository repository)
    : IRequestHandler<GetProductByIdQuery, ProductSummary>
{
    public async Task<ProductSummary> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ResourceNotFoundException("Product", request.Id);

        return new ProductSummary(product.Id, product.Name, product.Description,
            product.Price, product.StockQuantity);
    }
}
