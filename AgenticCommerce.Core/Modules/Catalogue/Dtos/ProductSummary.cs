namespace AgenticCommerce.Core.Modules.Catalogue.Dtos;

public sealed record ProductSummary(int Id, string Name, string Description, decimal Price, int StockQuantity);
