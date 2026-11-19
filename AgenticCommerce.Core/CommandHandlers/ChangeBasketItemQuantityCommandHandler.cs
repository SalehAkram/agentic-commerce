using AgenticCommerce.Core.Commands;
using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Dtos;
using AgenticCommerce.Core.Exceptions;
using AgenticCommerce.Core.Services;
using MediatR;

namespace AgenticCommerce.Core.CommandHandlers;

public sealed class ChangeBasketItemQuantityCommandHandler(IUnitOfWork unitOfWork, BasketResultMapper mapper)
    : IRequestHandler<ChangeBasketItemQuantityCommand, BasketResult>
{
    public async Task<BasketResult> Handle(ChangeBasketItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var basket = await unitOfWork.BasketRepository.GetActiveByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (basket is null)
        {
            throw new ResourceNotFoundException("Active basket containing product", request.ProductId);
        }

        basket.ChangeItemQuantity(request.ProductId, request.Quantity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var productIds = basket.Items.Select(item => item.ProductId).Distinct().ToArray();
        var products = await unitOfWork.ProductRepository.GetByIdsAsync(productIds, cancellationToken);
        return mapper.Map(request.CustomerId, basket, products);
    }
}
