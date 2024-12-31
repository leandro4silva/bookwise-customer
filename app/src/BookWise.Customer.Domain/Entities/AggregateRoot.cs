using Amazon.DynamoDBv2.DataModel;
using BookWise.Customer.Domain.Entities.Abstraction;
using BookWise.Customer.Domain.Events.Abstraction;

namespace BookWise.Customer.Domain.Entities;

public class AggregateRoot : IEntityBase
{
    [DynamoDBHashKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    private List<IDomainEvent> _events = new List<IDomainEvent>();

    public IEnumerable<IDomainEvent> Events => _events;

    protected void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }
}

