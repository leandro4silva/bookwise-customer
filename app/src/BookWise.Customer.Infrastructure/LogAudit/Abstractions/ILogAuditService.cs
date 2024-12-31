using BookWise.Customer.Infrastructure.LogAudit.Dtos;

namespace BookWise.Customer.Infrastructure.LogAudit.Abstractions;

public interface ILogAuditService
{
    Task AuditAsync(LogAuditCommand request);
}
