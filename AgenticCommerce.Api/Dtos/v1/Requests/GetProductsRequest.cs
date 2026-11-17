using System.ComponentModel.DataAnnotations;

namespace AgenticCommerce.Api.Dtos.v1.Requests;

public sealed class GetProductsRequest
{
    [StringLength(200)]
    public string? SearchTerm { get; init; }

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal? MinPrice { get; init; }

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal? MaxPrice { get; init; }

    public bool InStockOnly { get; init; }

    [Range(1, int.MaxValue)]
    public int PageNumber { get; init; } = 1;

    [Range(1, 100)]
    public int PageSize { get; init; } = 20;
}
