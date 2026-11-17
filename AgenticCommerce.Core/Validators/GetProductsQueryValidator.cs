using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Queries;

namespace AgenticCommerce.Core.Validators;

public sealed class GetProductsQueryValidator : IRequestValidator<GetProductsQuery>
{
    public IDictionary<string, string[]> Validate(GetProductsQuery request)
    {
        var errors = new Dictionary<string, string[]>();
        if (request.PageNumber < 1)
            errors[nameof(request.PageNumber)] = ["Page number must be at least 1."];

        if (request.PageSize is < 1 or > 100)
            errors[nameof(request.PageSize)] = ["Page size must be between 1 and 100."];

        if (errors.Count == 0 && ((long)request.PageNumber - 1) * request.PageSize > int.MaxValue)
            errors[nameof(request.PageNumber)] = ["Requested page exceeds the supported range."];

        if (request.SearchTerm?.Length > 200)
            errors[nameof(request.SearchTerm)] = ["Search term must be at most 200 characters."];

        if (request.MinPrice < 0)
            errors[nameof(request.MinPrice)] = ["Minimum price must not be negative."];

        if (request.MaxPrice < 0)
            errors[nameof(request.MaxPrice)] = ["Maximum price must not be negative."];

        if (request.MinPrice.HasValue && request.MaxPrice.HasValue && request.MinPrice > request.MaxPrice)
            errors[nameof(request.MaxPrice)] = ["Maximum price must be at least the minimum price."];

        return errors;
    }
}
