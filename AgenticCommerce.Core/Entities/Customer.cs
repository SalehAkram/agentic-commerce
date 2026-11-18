using AgenticCommerce.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace AgenticCommerce.Core.Entities;

public class Customer : Entity<Guid>, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    public Customer(string firstName, string lastName, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        email = email.Trim();
        if (!new EmailAddressAttribute().IsValid(email))
            throw new ArgumentException("A valid email address is required.", nameof(email));

        Id = Guid.NewGuid();
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email;
    }
}
