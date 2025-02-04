using System.Net;
using System.Text.Json;
using AutoMapper;
using BookWise.Customer.Application.Exceptions;
using BookWise.Customer.Application.Helpers;
using BookWise.Customer.Infrastructure.Auths.Abstractions;
using BookWise.Customer.Infrastructure.LogAudit.Abstractions;
using BookWise.Customer.Infrastructure.LogAudit.Dtos;
using BookWise.Customer.Infrastructure.LogAudit.Enums;
using BookWise.Customer.Infrastructure.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookWise.Customer.Application.Handlers.v1.ConfirmRegistrationCustomer;

public class ConfirmRegistrationCustomerHandler : IRequestHandler<ConfirmRegistrationCustomerCommand, ConfirmRegistrationCustomerResult>
{
    private readonly ICognitoService _cognitoService;
    private readonly IMapper _mapper;
    private readonly ILogAuditService _logAuditService;
    private readonly INotificationService _notificationService;
    private readonly ILogger<ConfirmRegistrationCustomerHandler> _logger;
    
    public ConfirmRegistrationCustomerHandler(ICognitoService cognitoService, IMapper mapper, ILogAuditService logAuditService, INotificationService notificationService, ILogger<ConfirmRegistrationCustomerHandler> logger)
    {
        _cognitoService = cognitoService;
        _mapper = mapper;
        _logAuditService = logAuditService;
        _notificationService = notificationService;
        _logger = logger;
    }
    
    public Task<ConfirmRegistrationCustomerResult> Handle(ConfirmRegistrationCustomerCommand request, CancellationToken cancellationToken)
    {
        _ = AuditarOperacao(request);
        
        return ConfirmRegistrationAsync(request, cancellationToken);
    }
    
    private async Task<ConfirmRegistrationCustomerResult> ConfirmRegistrationAsync(ConfirmRegistrationCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _cognitoService.GetCustomerAsync(request.Payload?.Email!, cancellationToken);
            
            NotFoundException.ThrowIfNull(user, "Usuario não encontrado");
            
            var confirm = await _cognitoService.ConfirmRegistrationAsync(request.Payload?.Email!, request.Payload?.ConfirmCode!, cancellationToken);
            
            return _mapper.Map<ConfirmRegistrationCustomerResult>(confirm);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido ao confirmar cadastro";
            NotificationHelper.Notificar(ex, ex.Message, _notificationService, _logger);
            throw new InternalServerErrorException(msg);
        }
    }
    
    private Task AuditarOperacao(object request)
    {
        var log = new LogAuditCommand(
            operacao: AuditoriaOperacao.Insercao,
            descricao: $"Confirm registration customer" + $"request: {JsonSerializer.Serialize(request)}");

        return _logAuditService.AuditAsync(log);
    }
}