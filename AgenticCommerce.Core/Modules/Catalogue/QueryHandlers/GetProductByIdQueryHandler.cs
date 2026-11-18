using AgenticCommerce.Core.Modules.Catalogue.Contracts;
using AgenticCommerce.Core.Shared.Contracts;
using AgenticCommerce.Core.Modules.Catalogue.Dtos;
using AgenticCommerce.Core.Shared.Exceptions;
using AgenticCommerce.Core.Modules.Catalogue.Queries;
using MediatR;

namespace AgenticCommerce.Core.Modules.Catalogue.QueryHandlers;

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
