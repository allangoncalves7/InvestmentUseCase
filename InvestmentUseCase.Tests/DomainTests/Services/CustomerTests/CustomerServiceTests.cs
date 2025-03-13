using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Services;
using InvestmentUseCase.Tests.Builders;
using Moq;

namespace InvestmentUseCase.Tests.DomainTests.Services.CustomerTests
{
    [Collection(nameof(CustomerServiceCollection))]
    public class CustomerServiceTests
    {
        private readonly CustomerService _customerService;
        private readonly CustomerServiceFixture _customerServiceFixture;

        public CustomerServiceTests(CustomerServiceFixture customerServiceFixture)
        {
            _customerServiceFixture = customerServiceFixture;
            _customerService = _customerServiceFixture.GetCustomerService();
        }

        [Fact(DisplayName = "Should get customer by id")]
        public async void ShouldGetCustomerById()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _customerServiceFixture.SetupGetById(customerId);

            // Act
            var customer = await _customerService.GetById(customerId);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(customerId, customer.Id);

        }

        [Fact(DisplayName = "Should get all customers")]
        public async void ShouldGetAllCustomers()
        {
            // Arrange
            _customerServiceFixture.SetupGetAll();
            // Act
            var customers = await _customerService.GetAll();
            // Assert
            Assert.NotNull(customers);
            Assert.NotEmpty(customers);
        }

        [Fact(DisplayName = "Should return true if customer exists")]
        public async void ShouldReturnTrueIfCustomerExists()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _customerServiceFixture.SetupExists(true);

            // Act
            var exists = await _customerService.Exists(customerId);

            // Assert
            Assert.True(exists);
        }

        [Fact(DisplayName = "Should return false if customer does not exist")]
        public async void ShouldReturnFalseIfCustomerDoesNotExist()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _customerServiceFixture.SetupExists(false);

            // Act
            var exists = await _customerService.Exists(customerId);

            // Assert
            Assert.False(exists);
        }

        [Fact(DisplayName = "Should return true if customer exists by account")]
        public async void ShouldReturnTrueIfCustomerExistsByAccount()
        {

            // Arrange
            var agency = "0001";
            var account = "12345";
            var dac = "6";

            _customerServiceFixture.SetupExists(true);

            // Act
            var exists = await _customerService.ExistsByAccount(agency, account, dac);

            // Assert
            Assert.True(exists);
        }

        [Fact(DisplayName = "Should return false if customer does not exist by account")]
        public async void ShouldReturnFalseIfCustomerDoesNotExistByAccount()
        {

            // Arrange
            var agency = "0001";
            var account = "12345";
            var dac = "6";

            _customerServiceFixture.SetupExists(false);

            // Act
            var exists = await _customerService.ExistsByAccount(agency, account, dac);

            // Assert
            Assert.False(exists);
        }

        [Fact(DisplayName = "Should add customer")]
        public async void ShouldAddCustomer()
        {
            // Arrange
            var customer = new CustomerBuilder().Create();
            _customerServiceFixture.SetupAdd(customer);

            // Act
            var added = await _customerService.Add(customer);

            // Assert
            Assert.Equal(customer, added);

        }

        [Fact(DisplayName = "Should update customer")]
        public async void ShouldUpdateCustomer()
        {
            // Arrange
            var customer = new CustomerBuilder().Create();
            _customerServiceFixture.SetupUpdate(customer);

            // Act
            var updated = await _customerService.Update(customer);

            // Assert
            Assert.Equal(customer, updated);
        }

        [Fact(DisplayName = "Should delete customer")]
        public async void ShouldDeleteCustomer()
        {
            // Arrange
            var customer = new CustomerBuilder().Create();
            _customerServiceFixture.SetupDelete();

            // Act
            await _customerService.Delete(customer);

            // Assert
            _customerServiceFixture.Mocker.GetMock<ICustomerRepository>()
                .Verify(x => x.DeleteAsync(It.IsAny<Customer>()), Times.Once);
        }
    }
}
