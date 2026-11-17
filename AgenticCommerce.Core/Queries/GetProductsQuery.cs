using AgenticCommerce.Core.Dtos;
using MediatR;

namespace AgenticCommerce.Core.Queries;

public sealed record GetProductsQuery(int PageNumber = 1, int PageSize = 20,string? SearchTerm = null, decimal? MinPrice = null, decimal? MaxPrice = null, bool InStockOnly = false)
    : IRequest<GetProductsResult>;
