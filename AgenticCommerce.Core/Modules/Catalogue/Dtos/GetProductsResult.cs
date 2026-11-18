namespace AgenticCommerce.Core.Modules.Catalogue.Dtos;

public sealed record GetProductsResult(
    IReadOnlyList<ProductSummary> Items, int PageNumber, int PageSize, int TotalCount);
