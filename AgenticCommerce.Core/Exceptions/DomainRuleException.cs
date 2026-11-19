namespace AgenticCommerce.Core.Exceptions;

public sealed class DomainRuleException(string message, string memberName) : Exception(message)
{
    public string MemberName { get; } = memberName;
}
