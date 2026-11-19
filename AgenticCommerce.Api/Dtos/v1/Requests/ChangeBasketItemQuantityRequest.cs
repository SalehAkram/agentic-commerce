using System.ComponentModel.DataAnnotations;

namespace AgenticCommerce.Api.Dtos.v1.Requests;

public class ChangeBasketItemQuantityRequest
{
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
