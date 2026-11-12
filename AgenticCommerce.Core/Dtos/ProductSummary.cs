namespace AgenticCommerce.Core.Dtos;

public sealed record ProductSummary(int Id, string Name, string Description, decimal Price, int StockQuantity);
