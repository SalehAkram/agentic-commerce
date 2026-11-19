using AgenticCommerce.Core.Dtos;
using MediatR;

namespace AgenticCommerce.Core.Commands;

public sealed record AddBasketItemCommand(Guid CustomerId, int ProductId, int Quantity) : IRequest<BasketResult>;
