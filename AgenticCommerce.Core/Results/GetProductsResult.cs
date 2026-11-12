namespace AgenticCommerce.Core.Results;

public sealed record GetProductsResult(
    IReadOnlyList<ProductSummary> Items, int PageNumber, int PageSize, int TotalCount);
