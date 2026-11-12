using AgenticCommerce.Core.Dtos;
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
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1)
            ModelState.AddModelError(nameof(pageNumber), "Page number must be at least 1.");

        if (pageSize is < 1 or > 100)
            ModelState.AddModelError(nameof(pageSize), "Page size must be between 1 and 100.");

        if (pageNumber >= 1 && pageSize is >= 1 and <= 100 &&
            ((long)pageNumber - 1) * pageSize > int.MaxValue)
            ModelState.AddModelError(nameof(pageNumber), "Requested page exceeds the supported range.");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await sender.Send(new GetProductsQuery(pageNumber, pageSize), cancellationToken);
        return Ok(result);
    }
}
