using Amazon.SQS;
using Amazon.SQS.Model;
using BookWise.Customer.Infrastructure.LogAudit.Abstractions;
using BookWise.Customer.Infrastructure.LogAudit.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BookWise.Customer.Infrastructure.LogAudit.Services;

public sealed class LogAuditService : ILogAuditService
{
    private readonly AuditoriaConfig _auditoriaConfig;
    private readonly ILogger<LogAuditService> _logger;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IAmazonSQS _amazonSQS;

    private const string ForwardedForHeaders = "x-forwarded-for";
    private const string OrigemDaOperacao = "bookwise-customer";

    public LogAuditService(
        IOptionsMonitor<AuditoriaConfig> auditoriaConfig,
        ILogger<LogAuditService> logger,
        IHttpContextAccessor contextAccessor,
        IAmazonSQS amazonSQS
        )
    {
        _auditoriaConfig = auditoriaConfig.CurrentValue;
        _logger = logger;
        _contextAccessor = contextAccessor;
        _amazonSQS = amazonSQS;
    }

    public async Task AuditAsync(LogAuditCommand request)
    {
        try
        {
            if (!_auditoriaConfig.Active)
            {
                _logger.LogWarning("Envio de log de auditoria esta desativado!");
                return;
            }

            var ip = GetRemoteIpAddress();

            var body = new AuditoriaSqsRequest()
            {
                DataHoraDaOperacao = DateTimeOffset.Now,
                Ip = ip,
                OrigemDaOperacao = OrigemDaOperacao,
                TipoOperacao = request.Operacao.ToString().ToString(),
            };

            var message = JsonSerializer.Serialize(body);

            var sendRequest = new SendMessageRequest(_auditoriaConfig.QueueUrl, message); 
            var sendResult = await _amazonSQS.SendMessageAsync(sendRequest);

            if (sendResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
                _logger.LogError("Houve um erro na publicacao de mensagem no SQS - HttpStatusCode: {0} - MessageBody: {1}", sendResult.HttpStatusCode, sendRequest.MessageBody);

            _logger.LogInformation($"Auditoria message: {message}");
        }
        catch ( Exception ex )
        {
            _logger.LogError(ex, ex.Message);
            if(ex.InnerException != null)
            {
                _logger.LogError(ex.InnerException, ex.InnerException.Message);
            }
        }
    }

    private string GetRemoteIpAddress()
    {
        var remoteIp = _contextAccessor.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        
        if (!string.IsNullOrWhiteSpace(remoteIp))
            return remoteIp;
        
        return _contextAccessor.HttpContext!.Request.Headers[ForwardedForHeaders].ToString();
    }
}
