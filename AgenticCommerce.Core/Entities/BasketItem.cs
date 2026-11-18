namespace AgenticCommerce.Core.Entities;

public class BasketItem : Entity<int>
{
    public int BasketId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }

    internal BasketItem(int productId, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(productId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ProductId = productId;
        Quantity = quantity;
    }

    internal void ChangeQuantity(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        Quantity = quantity;
    }
}
