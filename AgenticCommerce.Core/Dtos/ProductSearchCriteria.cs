namespace AgenticCommerce.Core.Dtos;

public sealed record ProductSearchCriteria(
    string? SearchTerm = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    bool InStockOnly = false);
