using BookWise.Customer.Domain.Events.Abstraction;
using BookWise.Customer.Domain.Events;
using System.Text;
using BookWise.Customer.Infrastructure.MessageBus.Abstraction;
using BookWise.Customer.Infrastructure.MessageBus.IntegrationEvents;
using BookWise.Customer.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace BookWise.Customer.Infrastructure.MessageBus.Event;

public class EventProcessor : IEventProcessor
{
    private readonly IPublisher _publisher;
    private readonly string _queueUrl = "https://sqs.sa-east-1.amazonaws.com/535002886987/bookwise-customer-created";

    public EventProcessor(
        IPublisher publisher,
        IOptionsMonitor<CreateCustomerSqsConfig> createCustomerSqsConfig)
    {
        _publisher = publisher;
    }

    public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
    {
        return events.Select(Map);
    }

    public IEvent Map(IDomainEvent @event)
    {
        return @event switch
        {
            CustomerCreated e => new CustomerCreatedIntegration(e.Id, e.FullName, e.Email),
            _ => throw new InvalidOperationException($"Evento não suportado: {@event.GetType().Name}")
        };
    }

    public async void Process(IEnumerable<IDomainEvent> events, string queueUrl, CancellationToken cancellationToken)
    {
        var integrationEvents = MapAll(events);

        foreach (var @event in integrationEvents)
        {
            await _publisher.PublishAsync(@event, _queueUrl, cancellationToken);
        }
    }

    private string MapConvention(IEvent @event)
    {
        return ToDashCase(@event.GetType().Name);
    }

    public string ToDashCase(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }
        if (text.Length < 2)
        {
            return text;
        }
        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('-');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        Console.WriteLine($"ToDashCase: " + sb.ToString());

        return sb.ToString();
    }
}
