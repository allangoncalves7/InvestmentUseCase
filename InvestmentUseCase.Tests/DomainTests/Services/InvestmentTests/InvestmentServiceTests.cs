using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Enums;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Services;
using InvestmentUseCase.Tests.Builders;
using Moq;

namespace InvestmentUseCase.Tests.DomainTests.Services.InvestmentTests
{
    [Collection(nameof(InvestmentServiceCollection))]
    public class InvestmentServiceTests
    {
        private readonly InvestmentService _investmentService;
        private readonly InvestmentServiceFixture _investmentServiceFixture;

        public InvestmentServiceTests(InvestmentServiceFixture investmentServiceFixture)
        {
            _investmentServiceFixture = investmentServiceFixture;
            _investmentService = _investmentServiceFixture.GetInvestmentService();
        }

        [Fact(DisplayName = "Should get investment by id")]
        public async void ShouldGetInvestmentById()
        {
            // Arrange
            var investmentId = Guid.NewGuid();
            _investmentServiceFixture.SetupGetById(investmentId);

            // Act
            var investment = await _investmentService.GetById(investmentId);

            // Assert
            Assert.NotNull(investment);
            Assert.Equal(investmentId, investment.Id);
        }

        [Fact(DisplayName = "Should get all investments")]
        public async void ShouldGetAllInvestments()
        {
            // Arrange
            _investmentServiceFixture.SetupGetAll();

            // Act
            var investments = await _investmentService.GetAll();

            // Assert
            Assert.NotNull(investments);
            Assert.NotEmpty(investments);
        }

        [Fact(DisplayName = "Should return true if investment exists")]
        public async void ShouldReturnTrueIfInvestmentExists()
        {
            // Arrange
            var investmentId = Guid.NewGuid();
            _investmentServiceFixture.SetupExists(true);

            // Act
            var exists = await _investmentService.Exists(investmentId);

            // Assert
            Assert.True(exists);
        }

        [Fact(DisplayName = "Should return false if investment not exists")]
        public async void ShouldReturnFalseIfInvestmentNotExists()
        {
            // Arrange
            var investmentId = Guid.NewGuid();
            _investmentServiceFixture.SetupExists(false);

            // Act
            var exists = await _investmentService.Exists(investmentId);

            // Assert
            Assert.False(exists);
        }

        [Fact(DisplayName = "Should return true if investment exists by customer")]
        public async void ShouldReturnTrueIfInvestmentExistsByCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var investmentProductId = Guid.NewGuid();
            _investmentServiceFixture.SetupExists(true);

            // Act
            var exists = await _investmentService.ExistsByCustomer(customerId, investmentProductId);

            // Assert
            Assert.True(exists);
        }

        [Fact(DisplayName = "Should return false if investment not exists by customer")]
        public async void ShouldReturnFalseIfInvestmentNotExistsByCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var investmentProductId = Guid.NewGuid();
            _investmentServiceFixture.SetupExists(false);

            // Act
            var exists = await _investmentService.ExistsByCustomer(customerId, investmentProductId);

            // Assert
            Assert.False(exists);
        }

        [Fact(DisplayName = "Should get by customer and product")]
        public async void ShouldGetByCustomerAndProduct()
        {
            // Arrange
            var agency = "123";
            var account = "456789";
            var dac = "1";
            var investmentProductId = Guid.NewGuid();

            _investmentServiceFixture.SetupGetByCustomerAndProduct();

            // Act
            var investment = await _investmentService.GetByCustomerAndProduct(agency, account, dac, investmentProductId);

            // Assert
            Assert.NotNull(investment);
        }

        [Fact(DisplayName = "Should get all by customer")]
        public async void ShouldGetAllByCustomer()
        {
            // Arrange
            var agency = "123";
            var account = "456789";
            var dac = "1";
            _investmentServiceFixture.SetupGetAllByCustomer();

            // Act
            var investments = await _investmentService.GetAllByCustomer(agency, account, dac);

            // Assert
            Assert.NotNull(investments);
            Assert.NotEmpty(investments);
        }


        [Fact(DisplayName = "Should add investment")]
        public async void ShouldAddInvestment()
        {
            // Arrange
            var investment = new InvestmentBuilder().CreateWithCustomerAndProduct();
            _investmentServiceFixture.SetupInclude(investment);
            _investmentServiceFixture.SetupAdd(investment);

            // Act
            var result = await _investmentService.Add(investment);

            // Assert
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Should update investment")]
        public async void ShouldUpdateInvestment()
        {
            // Arrange
            var investment = new InvestmentBuilder().Create();
            _investmentServiceFixture.SetupUpdate(investment);

            // Act
            var result = await _investmentService.Update(investment);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(investment.Id, result.Id);
        }

        [Fact(DisplayName = "Should update invested capital with deposit")]
        public async void ShouldUpdateInvestedCapitalWithDeposit()
        {
            // Arrange
            var amount = 1500;
            var action = EInvestmentAction.Deposit;
            var investment = new InvestmentBuilder().CreateWithCustomerAndProduct();
            var expectedCapital = amount + investment.InvestedCapital;
            _investmentServiceFixture.SetupInclude(investment);
            _investmentServiceFixture.SetupUpdate(investment);

            // Act
            var result = await _investmentService.UpdateInvestedCapital(investment.Id, action, amount);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCapital, result.InvestedCapital);
        }

        [Fact(DisplayName = "Should update invested capital with withdraw")]
        public async void ShouldUpdateInvestedCapitalWithWithdraw()
        {
            // Arrange
            var amount = 100;
            var action = EInvestmentAction.Withdraw;
            var investment = new InvestmentBuilder().CreateWithCustomerAndProduct();
            var tax = 0.1m;
            var expectedCapital = investment.InvestedCapital - (amount + (amount * tax));
            _investmentServiceFixture.SetupInclude(investment);
            _investmentServiceFixture.SetupUpdate(investment);

            // Act
            var result = await _investmentService.UpdateInvestedCapital(investment.Id, action, amount);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCapital, result.InvestedCapital);
        }

        [Fact(DisplayName = "Should throw ArgumentException when withdraw amount is zero or negative")]
        public void ShouldThrowArgumentExceptionWhenWithdrawAmountIsZeroOrNegative()
        {
            // Arrange
            var investment = new Investment(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1000m);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => investment.Withdraw(0));
            Assert.Throws<ArgumentException>(() => investment.Withdraw(-100));
        }

        [Fact(DisplayName = "Should throw InvalidOperationException when withdraw amount is greater than invested capital")]
        public void ShouldThrowInvalidOperationExceptionWhenWithdrawAmountIsGreaterThanInvestedCapital()
        {
            // Arrange
            var investment = new Investment(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 500m);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => investment.Withdraw(600));
        }

        [Fact(DisplayName = "Should throw InvalidOperationException when withdraw amount plus tax exceeds invested capital")]
        public void ShouldThrowInvalidOperationExceptionWhenWithdrawAmountPlusTaxExceedsInvestedCapital()
        {
            // Arrange
            var investment = new Investment(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 500m);
            var withdrawAmount = 460m;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => investment.Withdraw(withdrawAmount));
        }


        [Fact(DisplayName = "Should delete investment")]
        public async void ShouldDeleteInvestment()
        {
            // Arrange
            var investment = new InvestmentBuilder().Create();
            _investmentServiceFixture.SetupDelete();

            // Act
            await _investmentService.Delete(investment);

            // Assert
            _investmentServiceFixture.Mocker.GetMock<IInvestmentRepository>()
                .Verify(x => x.DeleteAsync(It.IsAny<Investment>()), Times.Once);
        }

    }
}
