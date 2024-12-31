namespace BookWise.Customer.Infrastructure.MessageBus.Abstraction;

public interface IPublisher
{
    Task PublishAsync(object message, string queueUrl, CancellationToken cancellationToken);
}
