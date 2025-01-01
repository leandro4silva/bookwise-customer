using AutoMapper;
using BookWise.Customer.Domain.Repositories;
using BookWise.Customer.Infrastructure.LogAudit.Abstractions;
using BookWise.Customer.Infrastructure.MessageBus.Abstraction;
using BookWise.Customer.Infrastructure.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookWise.Customer.Application.Handlers.v1.Customer.UpdateImage;

public sealed class UpdateImageCustomerHandler : IRequestHandler<UpdateImageCustomerCommand, UpdateImageCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<UpdateImageCustomerHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly ILogAuditService _logAuditService;

    public UpdateImageCustomerHandler(
        ICustomerRepository customerRepository, 
        ILogger<UpdateImageCustomerHandler> logger, 
        INotificationService notificationService, 
        IMapper mapper,
        ILogAuditService logAuditService    
    )
    {
        _customerRepository = customerRepository;
        _logger = logger;
        _notificationService = notificationService;
        _mapper = mapper;
        _logAuditService = logAuditService;
    }

    public Task<UpdateImageCustomerResult> Handle(UpdateImageCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
