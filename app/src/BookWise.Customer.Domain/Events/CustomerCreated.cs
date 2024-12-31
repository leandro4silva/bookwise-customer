using BookWise.Customer.Domain.Events.Abstraction;

namespace BookWise.Customer.Domain.Events;
public sealed class CustomerCreated : IDomainEvent
{
    public Guid Id { get; private set; }

    public string? FullName { get; private set; }

    public string? Email { get; private set; }
}
