using AutoMapper;
using BookWise.Customer.Application.Helpers;
using BookWise.Customer.Domain.Events;
using BookWise.Customer.Domain.Repositories;
using BookWise.Customer.Infrastructure.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using DomainEntity = BookWise.Customer.Domain.Entities;
using BookWise.Customer.Infrastructure.MessageBus.Abstraction;
using BookWise.Customer.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using BookWise.Customer.Infrastructure.LogAudit.Dtos;
using BookWise.Customer.Infrastructure.LogAudit.Enums;
using System.Text.Json;
using BookWise.Customer.Infrastructure.Auths.Abstractions;
using BookWise.Customer.Infrastructure.LogAudit.Abstractions;

namespace BookWise.Customer.Application.Handlers.v1.Customer.Create;

public sealed class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
{
    private readonly ILogger<CreateCustomerHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly IEventProcessor _eventProcessor;
    private readonly CreateCustomerSqsConfig _createCustomerSqsConfiguration;
    private readonly UserImageConfig _userImageConfig;
    private readonly ILogAuditService _logAuditService;
    private readonly ICognitoService _cognitoService;

    public CreateCustomerHandler(
        INotificationService notificationService,
        ILogger<CreateCustomerHandler> logger,
        IMapper mapper,
        IEventProcessor eventProcessor,
        IOptionsMonitor<CreateCustomerSqsConfig> createCustomerSqsConfiguration,
        IOptionsMonitor<UserImageConfig> userImageConfig,
        ILogAuditService logAuditService,
        ICognitoService cognitoService)
    {
        _notificationService = notificationService;
        _mapper = mapper;
        _logger = logger;
        _eventProcessor = eventProcessor;
        _createCustomerSqsConfiguration = createCustomerSqsConfiguration.CurrentValue;
        _userImageConfig = userImageConfig.CurrentValue;
        _logAuditService = logAuditService;
        _cognitoService = cognitoService;
    }

    public async Task<CreateCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        _ = AuditarOperacao(request);

        return await CreateCustomerAsync(request, cancellationToken);
    }

    private async Task<CreateCustomerResult> CreateCustomerAsync(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<DomainEntity.Customer>(request);
        customer.Image = _userImageConfig.ImageDefaultUrl;

        try
        {
            var customerCreatedEvent = _mapper.Map<CustomerCreated>(customer);

            customer.CreatedAddEvent(customerCreatedEvent);
            
            await _cognitoService.RegisterCustomerAsync(customer, cancellationToken);
            
            _eventProcessor.Process(customer.Events, _createCustomerSqsConfiguration.SqsQueueUrl!, cancellationToken);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido no cadastro de usuario";
            NotificationHelper.Notificar(ex, msg, _notificationService, _logger);
        }

        return _mapper.Map<CreateCustomerResult>(customer);
    }

    private Task AuditarOperacao(object request)
    {
        var log = new LogAuditCommand(
            operacao: AuditoriaOperacao.Insercao,
            descricao: $"Criacao do customer" + $"request: {JsonSerializer.Serialize(request)}");

        return _logAuditService.AuditAsync(log);
    }
}
