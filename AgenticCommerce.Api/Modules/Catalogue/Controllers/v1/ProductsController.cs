using AgenticCommerce.Core.Modules.Catalogue.Dtos;
using AgenticCommerce.Api.Modules.Catalogue.Dtos.v1.Requests;
using AgenticCommerce.Core.Modules.Catalogue.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgenticCommerce.Api.Modules.Catalogue.Controllers.v1;

[ApiController]
[Route("api/v1/products")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType<ProductSummary>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductSummary>> GetProductById(
        [FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetProductByIdQuery(id), cancellationToken);
        return Ok(result);
    }

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

