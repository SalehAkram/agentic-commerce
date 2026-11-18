namespace AgenticCommerce.Core.Dtos;

public sealed record GetProductsResult(
    IReadOnlyList<ProductSummary> Items, int PageNumber, int PageSize, int TotalCount);
