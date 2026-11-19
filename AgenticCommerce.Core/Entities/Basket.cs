using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Exceptions;

namespace AgenticCommerce.Core.Entities;

public class Basket : Entity<int>, IAggregateRoot
{
    private readonly List<BasketItem> _items = [];

    public Guid CustomerId { get; private set; }
    public BasketStatus Status { get; private set; } = BasketStatus.Active;
    public DateTime UpdatedAtUtc { get; private set; }
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

    public Basket(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("A customer ID is required.", nameof(customerId));
        CustomerId = customerId;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void AddItem(int productId, int quantity)
    {
        EnsureActive();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(productId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        var item = _items.SingleOrDefault(item => item.ProductId == productId);
        if (item is null)
            _items.Add(new BasketItem(productId, quantity));
        else
        {
            if ((long)item.Quantity + quantity > int.MaxValue)
                throw new DomainRuleException("Total quantity exceeds the supported range.", "Quantity");

            item.ChangeQuantity(item.Quantity + quantity);
        }
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void ChangeItemQuantity(int productId, int quantity)
    {
        EnsureActive();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(productId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        var item = _items.SingleOrDefault(item => item.ProductId == productId)
            ?? throw new InvalidOperationException("The product is not in this basket.");
        item.ChangeQuantity(quantity);
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void RemoveItem(int productId)
    {
        EnsureActive();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(productId);
        var item = _items.SingleOrDefault(item => item.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }

    private void EnsureActive()
    {
        if (Status != BasketStatus.Active)
            throw new InvalidOperationException("Only active baskets can be modified.");
    }
}
