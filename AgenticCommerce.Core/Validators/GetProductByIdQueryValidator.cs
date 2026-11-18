using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Queries;

namespace AgenticCommerce.Core.Validators;

public sealed class GetProductByIdQueryValidator : IRequestValidator<GetProductByIdQuery>
{
    public IDictionary<string, string[]> Validate(GetProductByIdQuery request)
    {
        var errors = new Dictionary<string, string[]>();
        if (request.Id < 1)
            errors[nameof(request.Id)] = ["Product ID must be at least 1."];
        return errors;
    }
}
