using DomainEntity = BookWise.Customer.Domain.Entities;

namespace BookWise.Customer.Domain.Repositories;

public interface ICustomerRepository
{
    Task AddAsync(DomainEntity.Customer customer, CancellationToken cancellationToken);

    Task Update(DomainEntity.Customer customer, CancellationToken cancellationToken);

    Task<DomainEntity.Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
