using AgenticCommerce.Core.Dtos;
using AgenticCommerce.Core.Entities;
using AgenticCommerce.Core.Exceptions;

namespace AgenticCommerce.Core.Services;

public sealed class BasketResultMapper
{
    public BasketResult Map(Guid customerId, Basket? basket, IReadOnlyCollection<Product> products)
    {
        if (basket is null)
        {
            return new BasketResult(null, customerId, null, null, Array.Empty<BasketItemResult>());
        }

        var productsById = products.ToDictionary(product => product.Id);
        var items = new List<BasketItemResult>();

        foreach (var item in basket.Items.OrderBy(item => item.ProductId))
        {
            if (!productsById.TryGetValue(item.ProductId, out var product))
            {
                throw new ResourceNotFoundException("Product", item.ProductId);
            }

            items.Add(new BasketItemResult(item.ProductId, item.Quantity)
            {
                Name = product.Name,
                UnitPrice = product.Price
            });
        }

        return new BasketResult(basket.Id, basket.CustomerId, basket.Status, basket.UpdatedAtUtc, items);
    }
}
