using AgenticCommerce.Core.Results;
using MediatR;

namespace AgenticCommerce.Core.Queries;

public sealed record GetProductsQuery(int PageNumber = 1, int PageSize = 20)
    : IRequest<GetProductsResult>;

