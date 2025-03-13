using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Services;
using InvestmentUseCase.Tests.Builders;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using System.Linq.Expressions;
using System.Xml;

namespace InvestmentUseCase.Tests.DomainTests.Services.CustomerTests
{
    [CollectionDefinition(nameof(CustomerServiceCollection))]
    public class CustomerServiceCollection : ICollectionFixture<CustomerServiceFixture>
    {
    }
    public class CustomerServiceFixture
    {

        public AutoMocker Mocker = new();
        public Mock<ILogger<CustomerService>> LoggerMock = new();

        public CustomerService GetCustomerService()
        {
            Mocker = new AutoMocker();
            LoggerMock = new Mock<ILogger<CustomerService>>();

            return Mocker.CreateInstance<CustomerService>();
        }

        public void SetupGetById(Guid id)
        {
            Mocker.GetMock<ICustomerRepository>()
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new CustomerBuilder().Create(id));
        }

        public void SetupGetAll()
        {
            Mocker.GetMock<ICustomerRepository>()
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync([new CustomerBuilder().Create()]);
        }

        public void SetupExists(bool exists)
        {
            Mocker.GetMock<ICustomerRepository>()
                .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync(exists);
        }

        public void SetupAdd(Customer customer)
        {
            Mocker.GetMock<ICustomerRepository>()
                .Setup(x => x.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);
        }

        public void SetupUpdate(Customer customer)
        {
            Mocker.GetMock<ICustomerRepository>()
                .Setup(x => x.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);
        }

        public void SetupDelete()
        {
            Mocker.GetMock<ICustomerRepository>()
                .Setup(x => x.DeleteAsync(It.IsAny<Customer>()))
                .Returns(Task.CompletedTask);
        }

    }
}
