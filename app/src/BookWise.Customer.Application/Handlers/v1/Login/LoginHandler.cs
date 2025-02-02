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
using UserNotConfirmedException = BookWise.Customer.Application.Exceptions.UserNotConfirmedException;

namespace BookWise.Customer.Application.Handlers.v1.Login;

public sealed class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly ICognitoService _cognitoService;
    private readonly IMapper _mapper;
    private readonly ILogAuditService _logAuditService;
    private readonly INotificationService _notificationService;
    private readonly ILogger<LoginHandler> _logger;
    
    public LoginHandler(
        ICognitoService cognitoService, 
        IMapper mapper, 
        ILogAuditService logAuditService, 
        INotificationService notificationService, 
        ILogger<LoginHandler> logger)
    {
        _cognitoService = cognitoService;
        _mapper = mapper;
        _logAuditService = logAuditService;
        _notificationService = notificationService;
        _logger = logger;
    }
    
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _ = AuditarOperacao(request);
        
        return await LoginAsync(request, cancellationToken);
    }

    private async Task<LoginResult> LoginAsync(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _cognitoService.GetCustomerAsync(request.Payload?.Email!, cancellationToken);

            var customer = _mapper.Map<Domain.Entities.Customer>(user);

            customer.Password = request.Payload?.Password;

            var token = await _cognitoService.LoginCustomerAsync(customer, cancellationToken);
            
            return _mapper.Map<LoginResult>(token);
        }
        catch (Amazon.CognitoIdentityProvider.Model.UserNotConfirmedException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            var msg = "Customer não está confirmado";
            NotificationHelper.Notificar(ex, ex.Message, _notificationService, _logger);
            throw new UserNotConfirmedException(msg);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido ao fazer login";
            NotificationHelper.Notificar(ex, ex.Message, _notificationService, _logger);
            throw new InternalServerErrorException(msg);
        }
    }
    
    private Task AuditarOperacao(object request)
    {
        var log = new LogAuditCommand(
            operacao: AuditoriaOperacao.Insercao,
            descricao: $"Update Imagem customer" + $"request: {JsonSerializer.Serialize(request)}");

        return _logAuditService.AuditAsync(log);
    }
}