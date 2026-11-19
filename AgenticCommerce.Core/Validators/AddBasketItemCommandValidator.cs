
using AgenticCommerce.Core.Commands;
using AgenticCommerce.Core.Contracts;

namespace AgenticCommerce.Core.Validators;

public sealed class AddBasketItemCommandValidator : IRequestValidator<AddBasketItemCommand>
{
    public IDictionary<string, string[]> Validate(AddBasketItemCommand request)
    {
        var errors = new Dictionary<string, string[]>();
        if (request.CustomerId == Guid.Empty)
            errors[nameof(request.CustomerId)] = ["Customer ID is required."];
        if (request.ProductId < 1)
            errors[nameof(request.ProductId)] = ["Product ID must be at least 1."];
        if (request.Quantity < 1)
            errors[nameof(request.Quantity)] = ["Quantity must be at least 1."];
        return errors;
    }
}
