using AgenticCommerce.Core.Dtos;
using AgenticCommerce.Api.Dtos.v1.Requests;
using AgenticCommerce.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgenticCommerce.Api.Controllers.v1;

[ApiController]
[Route("api/v1/products")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<GetProductsResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetProductsResult>> GetProducts(
        [FromQuery] GetProductsRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = new GetProductsQuery(request.PageNumber, request.PageSize,
            request.SearchTerm, request.MinPrice, request.MaxPrice, request.InStockOnly);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }
}

