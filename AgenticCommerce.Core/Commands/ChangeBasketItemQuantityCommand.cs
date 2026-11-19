using AgenticCommerce.Core.Dtos;
using MediatR;

namespace AgenticCommerce.Core.Commands;

public sealed record ChangeBasketItemQuantityCommand(Guid CustomerId, int ProductId, int Quantity) : IRequest<BasketResult>;

