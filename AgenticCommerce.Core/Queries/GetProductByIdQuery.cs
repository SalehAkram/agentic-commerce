using AgenticCommerce.Core.Dtos;
using MediatR;

namespace AgenticCommerce.Core.Queries;

public sealed record GetProductByIdQuery(int Id) : IRequest<ProductSummary>;
