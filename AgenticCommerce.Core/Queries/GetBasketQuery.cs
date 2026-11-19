using AgenticCommerce.Core.Dtos;
using MediatR;

namespace AgenticCommerce.Core.Queries;

public sealed record GetBasketQuery(Guid CustomerId) : IRequest<BasketResult>;
