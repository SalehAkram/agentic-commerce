using AgenticCommerce.Api.Dtos.v1.Requests;
using AgenticCommerce.Core.Commands;
using AgenticCommerce.Core.Queries;
using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Dtos;
using AgenticCommerce.Infra.Seeding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgenticCommerce.Api.Controllers.v1;

[Route("api/v1/basket")]
[ApiController]
public class BasketController(ISender sender, IUnitOfWork unitOfWork, IWebHostEnvironment environment) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<BasketResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<BasketResult>> GetBasket(CancellationToken cancellationToken = default)
    {
        if (!environment.IsDevelopment())
        {
            return Problem(statusCode: StatusCodes.Status403Forbidden, detail: "Development customer access is disabled.");
        }

        var customerId = await GetDevelopmentCustomerIdAsync(cancellationToken);
        var result = await sender.Send(new GetBasketQuery(customerId), cancellationToken);
        return Ok(result);
    }

    [HttpPost("items")]
    [ProducesResponseType<BasketResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BasketResult>> AddItem([FromBody] AddBasketItemRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!environment.IsDevelopment())
            return Problem(statusCode: StatusCodes.Status403Forbidden, detail: "Development customer access is disabled.");

        var customerId = await GetDevelopmentCustomerIdAsync(cancellationToken);
        var result = await sender.Send(new AddBasketItemCommand(customerId, request.ProductId, request.Quantity), cancellationToken);
        return Ok(result);
    }

    private async Task<Guid> GetDevelopmentCustomerIdAsync(CancellationToken cancellationToken)
    {
        // Replace this development identity lookup with validated authentication when introduced.
        var customer = await unitOfWork.CustomerRepository.FindByAsync(
            customer => customer.Email == CustomerSeeder.DevelopmentCustomerEmail, cancellationToken)
            ?? throw new InvalidOperationException("The development customer has not been seeded.");

        return customer.Id;
    }
}
