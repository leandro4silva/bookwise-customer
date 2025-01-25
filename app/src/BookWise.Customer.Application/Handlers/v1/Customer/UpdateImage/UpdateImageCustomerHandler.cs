using AutoMapper;
using BookWise.Customer.Application.Helpers;
using BookWise.Customer.Domain.Repositories;
using BookWise.Customer.Infrastructure.Buckets.Abstractions;
using DomainEntity = BookWise.Customer.Domain.Entities;
using BookWise.Customer.Infrastructure.LogAudit.Abstractions;
using BookWise.Customer.Infrastructure.LogAudit.Dtos;
using BookWise.Customer.Infrastructure.LogAudit.Enums;
using BookWise.Customer.Infrastructure.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using BookWise.Customer.Application.Exceptions;

namespace BookWise.Customer.Application.Handlers.v1.Customer.UpdateImage;

public sealed class UpdateImageCustomerHandler : IRequestHandler<UpdateImageCustomerCommand, UpdateImageCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<UpdateImageCustomerHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly ILogAuditService _logAuditService;
    private readonly IBucketS3Service _bucketS3Service;

    public UpdateImageCustomerHandler(
        ICustomerRepository customerRepository, 
        ILogger<UpdateImageCustomerHandler> logger, 
        INotificationService notificationService, 
        IMapper mapper,
        ILogAuditService logAuditService,
        IBucketS3Service bucketS3Service
    )
    {
        _customerRepository = customerRepository;
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

            var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);

            NotFoundException.ThrowIfNull(customer, "Usuario nao encontrado");
            
            customer.Image = imageUrl;

            await _customerRepository.Update(customer, cancellationToken);

            return _mapper.Map<UpdateImageCustomerResult>(customer);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido ao atualizar imagem do usuario";
            NotificationHelper.Notificar(ex, msg, _notificationService, _logger);
        }

        return _mapper.Map<UpdateImageCustomerResult>(request);
    }

    private Task AuditarOperacao(object request)
    {
        var log = new LogAuditCommand(
            operacao: AuditoriaOperacao.Insercao,
            descricao: $"Update Imagem customer" + $"request: {JsonSerializer.Serialize(request)}");

        return _logAuditService.AuditAsync(log);
    }
}
