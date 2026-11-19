using AgenticCommerce.Core.Entities;

namespace AgenticCommerce.Core.Dtos;

public sealed record BasketResult(int Id, Guid CustomerId, BasketStatus Status,
    DateTime UpdatedAtUtc, IReadOnlyList<BasketItemResult> Items);
