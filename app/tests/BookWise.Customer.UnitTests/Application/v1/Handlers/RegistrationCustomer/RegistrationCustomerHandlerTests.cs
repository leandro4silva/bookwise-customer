﻿using AutoMapper;
using BookWise.Customer.Application.Handlers.v1.RegistrationCustomer;
using BookWise.Customer.Domain.Events;
using BookWise.Customer.Domain.Events.Abstraction;
using BookWise.Customer.Infrastructure.Auths.Abstractions;
using BookWise.Customer.Infrastructure.Configurations;
using BookWise.Customer.Infrastructure.LogAudit.Abstractions;
using BookWise.Customer.Infrastructure.MessageBus.Abstraction;
using BookWise.Customer.Infrastructure.Notifications.Abstraction;
using BookWise.Customer.UtilTests.Builders.Application.v1.Command;
using BookWise.Customer.UtilTests.Builders.Application.v1.Result;
using BookWise.Customer.UtilTests.Builders.Domain;
using BookWise.Customer.UtilTests.Builders.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using DomainEntity = BookWise.Customer.Domain.Entities;

namespace BookWise.Customer.UnitTests.Application.v1.Handlers.RegistrationCustomer;

public class RegistrationCustomerHandlerTests
{
    private readonly Mock<ICognitoService> _cognitoServiceMock;
    private readonly Mock<ILogger<RegistrationCustomerHandler>> _loggerMock;
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IEventProcessor> _eventProcessorMock;
    private readonly Mock<IOptionsMonitor<CreateCustomerSqsConfig>> _createCustomerSqsConfigurationMock;
    private readonly Mock<IOptionsMonitor<UserImageConfig>> _userImageConfigMock;
    private readonly Mock<ILogAuditService> _logAuditServiceMock;
    private readonly RegistrationCustomerHandler _handler;

    public RegistrationCustomerHandlerTests()
    {
        _cognitoServiceMock = new Mock<ICognitoService>();
        _loggerMock = new Mock<ILogger<RegistrationCustomerHandler>>();
        _notificationServiceMock = new Mock<INotificationService>();
        _mapperMock = new Mock<IMapper>();
        _eventProcessorMock = new Mock<IEventProcessor>();
        _createCustomerSqsConfigurationMock = new Mock<IOptionsMonitor<CreateCustomerSqsConfig>>();
        _userImageConfigMock = new Mock<IOptionsMonitor<UserImageConfig>>();
        _logAuditServiceMock = new Mock<ILogAuditService>();

        var userImageConfig = UserImageConfigBuilder.Instance.Build();
        var createCustomerSqsConfig = CreateCustomerSqsConfigBuilder.Instance.Build();

        _userImageConfigMock.Setup(x => x.CurrentValue).Returns(userImageConfig);
        _createCustomerSqsConfigurationMock.Setup(x => x.CurrentValue).Returns(createCustomerSqsConfig);

        _handler = new RegistrationCustomerHandler(
            _notificationServiceMock.Object, 
            _loggerMock.Object,
            _mapperMock.Object,
            _eventProcessorMock.Object,
            _createCustomerSqsConfigurationMock.Object,
            _userImageConfigMock.Object,
            _logAuditServiceMock.Object,
            _cognitoServiceMock.Object
        );
    }

    [Fact(DisplayName = nameof(RegistrationCustomerHandler) + nameof(RegistrationCustomerHandler.Handle) + ": Success")]
    public async Task Should_Return_CustomerId_When_Success()
    {
        //Arrange
        var command = CreateCustomerCommandBuilder.Instance.Build();
        var customer = CustomerBuilder.Instance.Build();
        var customerCreated = CustomerCreatedBuilder.Instance.Build();
        
        var result = CreateCustomerResultBuilder.Instance.Build();

        _mapperMock.Setup(x => x.Map<DomainEntity.Customer>(It.IsAny<RegistrationCustomerCommand>())).Returns(customer);
        _mapperMock.Setup(x => x.Map<CustomerCreated>(It.IsAny<DomainEntity.Customer>())).Returns(customerCreated);
        _mapperMock.Setup(x => x.Map<RegistrationCustomerResult>(It.IsAny<DomainEntity.Customer>())).Returns(result);

        _cognitoServiceMock.Setup(x => x.RegisterCustomerAsync(It.IsAny<DomainEntity.Customer>(), It.IsAny<CancellationToken>()));

        _eventProcessorMock.Setup(x => x.Process(It.IsAny<IEnumerable<IDomainEvent>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        //Act
        var response = await _handler.Handle(command, CancellationToken.None);

        //Asserts
        response.Should().NotBeNull();
        response.Id.Should().NotBeEmpty();
    }
}
