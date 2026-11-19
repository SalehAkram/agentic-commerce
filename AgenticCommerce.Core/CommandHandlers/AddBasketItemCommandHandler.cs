using AgenticCommerce.Core.Commands;
using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Dtos;
using AgenticCommerce.Core.Entities;
using AgenticCommerce.Core.Exceptions;
using MediatR;

namespace AgenticCommerce.Core.CommandHandlers;

public class AddBasketItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddBasketItemCommand, BasketResult>
{
    public async Task<BasketResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
            throw new RequestValidationException(new Dictionary<string, string[]>
            {
                [nameof(request.CustomerId)] = ["Customer does not exist."]
            });

        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            throw new ResourceNotFoundException("Product", request.ProductId);

        var basket = await unitOfWork.BasketRepository.GetActiveByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (basket is null)
        {
            basket = new Basket(request.CustomerId);
            unitOfWork.BasketRepository.Add(basket);
        }

        basket.AddItem(request.ProductId, request.Quantity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var items = basket.Items.OrderBy(item => item.ProductId)
            .Select(item => new BasketItemResult(item.ProductId, item.Quantity)).ToArray();
        return new BasketResult(basket.Id, basket.CustomerId, basket.Status, basket.UpdatedAtUtc, items);
    }
}
