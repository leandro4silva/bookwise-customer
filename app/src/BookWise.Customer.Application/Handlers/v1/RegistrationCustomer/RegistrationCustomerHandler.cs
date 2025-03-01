﻿using System.Text.Json;
using AutoMapper;
using BookWise.Customer.Application.Exceptions;
using BookWise.Customer.Application.Helpers;
using BookWise.Customer.Domain.Events;
using BookWise.Customer.Infrastructure.Auths.Abstractions;
using BookWise.Customer.Infrastructure.Configurations;
using BookWise.Customer.Infrastructure.LogAudit.Abstractions;
using BookWise.Customer.Infrastructure.LogAudit.Dtos;
using BookWise.Customer.Infrastructure.LogAudit.Enums;
using BookWise.Customer.Infrastructure.MessageBus.Abstraction;
using BookWise.Customer.Infrastructure.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DomainEntity = BookWise.Customer.Domain.Entities;

namespace BookWise.Customer.Application.Handlers.v1.RegistrationCustomer;

public sealed class RegistrationCustomerHandler : IRequestHandler<RegistrationCustomerCommand, RegistrationCustomerResult>
{
    private readonly ILogger<RegistrationCustomerHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly IEventProcessor _eventProcessor;
    private readonly CreateCustomerSqsConfig _createCustomerSqsConfiguration;
    private readonly UserImageConfig _userImageConfig;
    private readonly ILogAuditService _logAuditService;
    private readonly ICognitoService _cognitoService;

    public RegistrationCustomerHandler(
        INotificationService notificationService,
        ILogger<RegistrationCustomerHandler> logger,
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

    public async Task<RegistrationCustomerResult> Handle(RegistrationCustomerCommand request, CancellationToken cancellationToken)
    {
        _ = AuditarOperacao(request);

        return await CreateCustomerAsync(request, cancellationToken);
    }

    private async Task<RegistrationCustomerResult> CreateCustomerAsync(RegistrationCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<DomainEntity.Customer>(request);
        customer.Image = _userImageConfig.ImageDefaultUrl;
        
        try
        {
            var customerCreatedEvent = _mapper.Map<CustomerCreated>(customer);

            customer.CreatedAddEvent(customerCreatedEvent);
            
            await _cognitoService.RegisterCustomerAsync(customer, cancellationToken);
            
            _eventProcessor.Process(customer.Events, _createCustomerSqsConfiguration.SqsQueueUrl!, cancellationToken);
            
            return _mapper.Map<RegistrationCustomerResult>(customer);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido no cadastro de usuario";
            NotificationHelper.Notificar(ex, msg, _notificationService, _logger);
            throw new InternalServerErrorException(msg);
        }
    }

    private Task AuditarOperacao(object request)
    {
        var log = new LogAuditCommand(
            operacao: AuditoriaOperacao.Insercao,
            descricao: $"Criacao do customer" + $"request: {JsonSerializer.Serialize(request)}");

        return _logAuditService.AuditAsync(log);
    }
}
