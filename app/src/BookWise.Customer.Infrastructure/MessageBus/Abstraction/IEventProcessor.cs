using BookWise.Customer.Domain.Events.Abstraction;

namespace BookWise.Customer.Infrastructure.MessageBus.Abstraction;

public interface IEventProcessor
{
    void Process(IEnumerable<IDomainEvent> events, string queueUrl, CancellationToken cancellationToken);
}
