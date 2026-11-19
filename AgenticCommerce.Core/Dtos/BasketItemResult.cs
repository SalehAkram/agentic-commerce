namespace AgenticCommerce.Core.Dtos;

public sealed record BasketItemResult(int ProductId, int Quantity)
{
    public string Name { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public decimal LineTotal => UnitPrice * Quantity;
}
