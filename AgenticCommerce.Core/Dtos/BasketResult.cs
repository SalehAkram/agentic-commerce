using AgenticCommerce.Core.Entities;
using System.Text.Json.Serialization;

namespace AgenticCommerce.Core.Dtos;

public sealed record BasketResult(int? Id, Guid CustomerId,
    [property: JsonConverter(typeof(JsonStringEnumConverter<BasketStatus>))] BasketStatus? Status,
    DateTime? UpdatedAtUtc, IReadOnlyList<BasketItemResult> Items)
{
    public string Currency { get; init; } = "GBP";
    public decimal Total => Items.Sum(item => item.LineTotal);
}
