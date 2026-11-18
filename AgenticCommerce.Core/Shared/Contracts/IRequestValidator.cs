namespace AgenticCommerce.Core.Shared.Contracts;

public interface IRequestValidator<in TRequest>
{
    IDictionary<string, string[]> Validate(TRequest request);
}
