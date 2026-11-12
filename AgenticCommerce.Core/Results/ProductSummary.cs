namespace AgenticCommerce.Core.Results;

public sealed record ProductSummary(int Id, string Name, string Description, decimal Price, int StockQuantity);
