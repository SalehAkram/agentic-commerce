using System.ComponentModel.DataAnnotations;

namespace AgenticCommerce.Api.Dtos.v1.Requests;

public sealed class GetProductsRequest
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; init; } = 1;

    [Range(1, 100)]
    public int PageSize { get; init; } = 20;
}
