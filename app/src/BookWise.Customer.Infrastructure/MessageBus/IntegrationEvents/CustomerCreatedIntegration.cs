using BookWise.Customer.Infrastructure.MessageBus.Abstraction;

namespace BookWise.Customer.Infrastructure.MessageBus.IntegrationEvents;

public sealed class CustomerCreatedIntegration : IEvent
{
    public Guid Id { get; private set; }

    public string? FullName { get; private set; }

    public string? Email { get; private set; }

    public CustomerCreatedIntegration(Guid id, string? fullName, string? email)
    {
        Id = id;
        FullName = fullName;
        Email = email;
    }
}
