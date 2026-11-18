namespace AgenticCommerce.Core.Exceptions;

public sealed class ResourceNotFoundException(string resourceName, int resourceId)
    : Exception($"{resourceName} with ID {resourceId} was not found.")
{
    public string ResourceName { get; } = resourceName;
    public int ResourceId { get; } = resourceId;
}
