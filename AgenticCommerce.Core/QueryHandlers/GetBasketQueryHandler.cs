using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Dtos;
using AgenticCommerce.Core.Exceptions;
using AgenticCommerce.Core.Queries;
using AgenticCommerce.Core.Services;
using MediatR;

namespace AgenticCommerce.Core.QueryHandlers;

public sealed class GetBasketQueryHandler(IUnitOfWork unitOfWork, BasketResultMapper mapper)
    : IRequestHandler<GetBasketQuery, BasketResult>
{
    public async Task<BasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            throw new RequestValidationException(new Dictionary<string, string[]>
            {
                [nameof(request.CustomerId)] = ["Customer does not exist."]
            });
        }

        var basket = await unitOfWork.BasketRepository.GetActiveByCustomerIdAsync(request.CustomerId, cancellationToken);
        var productIds = basket?.Items.Select(item => item.ProductId).Distinct().ToArray() ?? [];
        var products = await unitOfWork.ProductRepository.GetByIdsAsync(productIds, cancellationToken);

        return mapper.Map(request.CustomerId, basket, products);
    }
}
