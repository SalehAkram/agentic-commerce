using System.ComponentModel.DataAnnotations;

namespace AgenticCommerce.Api.Dtos.v1.Requests;

public class AddBasketItemRequest
{
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
