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
        var result = await sender.Send(new GetProductsQuery(request.PageNumber, request.PageSize), cancellationToken);
        return Ok(result);
    }
}

