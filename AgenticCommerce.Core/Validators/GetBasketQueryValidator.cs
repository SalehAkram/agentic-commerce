using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Queries;

namespace AgenticCommerce.Core.Validators;

public sealed class GetBasketQueryValidator : IRequestValidator<GetBasketQuery>
{
    public IDictionary<string, string[]> Validate(GetBasketQuery request)
    {
        var errors = new Dictionary<string, string[]>();
        if (request.CustomerId == Guid.Empty)
        {
            errors[nameof(request.CustomerId)] = ["Customer ID is required."];
        }

        return errors;
    }
}
