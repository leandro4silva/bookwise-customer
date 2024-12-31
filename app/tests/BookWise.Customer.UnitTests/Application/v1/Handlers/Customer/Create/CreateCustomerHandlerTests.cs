using BookWise.Customer.Application.Handlers.v1.Customer.Create;
using BookWise.Customer.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookWise.Customer.UnitTests.Application.v1.Handlers.Customer.Create;

public class CreateCustomerHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepository;
    private readonly Mock<ILogger<CreateCustomerHandler>>

    public CreateCustomerHandlerTests()
    {
        _customerRepository = new Mock<ICustomerRepository>();
    }
}
