using Amazon.SQS;
using Amazon.SQS.Model;
using BookWise.Customer.Infrastructure.MessageBus.Abstraction;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookWise.Customer.Infrastructure.MessageBus.Clients;

public sealed class AwsSQSClient : IPublisher
{
    private readonly IAmazonSQS _sqsClient;
    private readonly ILogger<AwsSQSClient> _logger;

    public AwsSQSClient(IAmazonSQS sqsClient, ILogger<AwsSQSClient> logger)
    {
        _sqsClient = sqsClient;
        _logger = logger;
    }

    public async Task PublishAsync(object message, string queueUrl, CancellationToken cancellationToken)
    {
        try
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var payload = JsonConvert.SerializeObject(message, settings);

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = payload
            };

            var response = await _sqsClient.SendMessageAsync(sendMessageRequest);

            _logger.LogInformation("Message sent with ID: {MessageId}", response.MessageId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{class} - {method} - Falha - " +
                "Mensagem {@mensagem}",
                nameof(AwsSQSClient), nameof(PublishAsync), ex.Message);

            throw;
        }
    }
}
