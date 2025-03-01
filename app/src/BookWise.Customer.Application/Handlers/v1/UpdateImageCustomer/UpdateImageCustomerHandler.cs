﻿using System.Text.Json;
using AutoMapper;
using BookWise.Customer.Application.Exceptions;
using BookWise.Customer.Application.Helpers;
using BookWise.Customer.Infrastructure.Auths.Abstractions;
using BookWise.Customer.Infrastructure.Buckets.Abstractions;
using BookWise.Customer.Infrastructure.LogAudit.Abstractions;
using BookWise.Customer.Infrastructure.LogAudit.Dtos;
using BookWise.Customer.Infrastructure.LogAudit.Enums;
using BookWise.Customer.Infrastructure.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookWise.Customer.Application.Handlers.v1.UpdateImageCustomer;

public sealed class UpdateImageCustomerHandler : IRequestHandler<UpdateImageCustomerCommand, UpdateImageCustomerResult>
{
    private readonly ICognitoService _cognitoService;
    private readonly ILogger<UpdateImageCustomerHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly ILogAuditService _logAuditService;
    private readonly IBucketS3Service _bucketS3Service;

    public UpdateImageCustomerHandler(
        ICognitoService cognitoService, 
        ILogger<UpdateImageCustomerHandler> logger, 
        INotificationService notificationService, 
        IMapper mapper,
        ILogAuditService logAuditService,
        IBucketS3Service bucketS3Service
    )
    {
        _cognitoService = cognitoService;
        _logger = logger;
        _notificationService = notificationService;
        _mapper = mapper;
        _logAuditService = logAuditService;
        _bucketS3Service = bucketS3Service;
    }

    public async Task<UpdateImageCustomerResult> Handle(UpdateImageCustomerCommand request, CancellationToken cancellationToken)
    {
        _ = AuditarOperacao(request);

        return await UpdateImageCustomerAsync(request, cancellationToken);
    }

    private async Task<UpdateImageCustomerResult> UpdateImageCustomerAsync(UpdateImageCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var tempFilePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(tempFilePath))
            {
                await request.Image!.CopyToAsync(stream);
            }

            var key = Guid.NewGuid().ToString() + Path.GetExtension(request.Image!.FileName);

            var imageUrl = await _bucketS3Service.UploadFileAsync(tempFilePath, key, cancellationToken);

            await _cognitoService.UpdateImageCustomerAsync(request.Email!, imageUrl, cancellationToken);
            
            var customer = await _cognitoService.GetCustomerAsync(request.Email!, cancellationToken);
            
            return _mapper.Map<UpdateImageCustomerResult>(customer);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido ao atualizar imagem do usuario";
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
