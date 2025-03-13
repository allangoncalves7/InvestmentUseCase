using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Services;
using InvestmentUseCase.Tests.Builders;
using Moq;

namespace InvestmentUseCase.Tests.DomainTests.Services.InvestmentProductTests
{
    [Collection(nameof(InvestmentProductServiceCollection))]
    public class InvestmentProductServiceTests
    {
        private readonly InvestmentProductService _investmentProductService;
        private readonly InvestmentProductServiceFixture _investmentProductServiceFixture;
        public InvestmentProductServiceTests(InvestmentProductServiceFixture investmentProductServiceFixture)
        {
            _investmentProductServiceFixture = investmentProductServiceFixture;
            _investmentProductService = _investmentProductServiceFixture.GetInvestmentProductService();
        }

        [Fact(DisplayName = "Should get investment product by id")]
        public async void ShouldGetInvestmentProductById()
        {
            // Arrange
            var investmentProductId = Guid.NewGuid();
            _investmentProductServiceFixture.SetupGetById(investmentProductId);

            // Act
            var investmentProduct = await _investmentProductService.GetById(investmentProductId);

            // Assert
            Assert.NotNull(investmentProduct);
            Assert.Equal(investmentProductId, investmentProduct.Id);
        }
        [Fact(DisplayName = "Should get all investment products")]
        public async void ShouldGetAllInvestmentProducts()
        {
            // Arrange
            _investmentProductServiceFixture.SetupGetAll();

            // Act
            var investmentProducts = await _investmentProductService.GetAll();

            // Assert
            Assert.NotNull(investmentProducts);
            Assert.NotEmpty(investmentProducts);
        }
        [Fact(DisplayName = "Should return true if investment product exists")]
        public async void ShouldReturnTrueIfInvestmentProductExists()
        {
            // Arrange
            var investmentProductId = Guid.NewGuid();
            _investmentProductServiceFixture.SetupExists(true);

            // Act
            var exists = await _investmentProductService.Exists(investmentProductId);

            // Assert
            Assert.True(exists);
        }

        [Fact(DisplayName = "Should return false if investment product does not exist")]
        public async void ShouldReturnFalseIfInvestmentProductDoesNotExist()
        {
            // Arrange
            var investmentProductId = Guid.NewGuid();
            _investmentProductServiceFixture.SetupExists(false);

            // Act
            var exists = await _investmentProductService.Exists(investmentProductId);

            // Assert
            Assert.False(exists);
        }

        [Fact(DisplayName = "Should return true if investment product exist by code")]
        public async void ShouldReturnTrueIfInvestmentProductExistByCode()
        {
            // Arrange
            var investmentProductCode = "code";
            _investmentProductServiceFixture.SetupExists(true);

            // Act
            var exists = await _investmentProductService.ExistsByCode(investmentProductCode);

            // Assert
            Assert.True(exists);
        }

        [Fact(DisplayName = "Should return false if investment product does not exist by code")]
        public async void ShouldReturnFalseIfInvestmentProductDoesNotExistByCode()
        {
            // Arrange
            var investmentProductCode = "code";
            _investmentProductServiceFixture.SetupExists(false);

            // Act
            var exists = await _investmentProductService.ExistsByCode(investmentProductCode);

            // Assert
            Assert.False(exists);
        }

        [Fact(DisplayName = "Should add investment product")]
        public async void ShouldAddInvestmentProduct()
        {
            // Arrange
            var investmentProduct = new InvestmentProductBuilder().Create();
            _investmentProductServiceFixture.SetupAdd(investmentProduct);

            // Act
            var addedInvestmentProduct = await _investmentProductService.Add(investmentProduct);

            // Assert
            Assert.NotNull(addedInvestmentProduct);
            Assert.Equal(investmentProduct, addedInvestmentProduct);
        }

        [Fact(DisplayName = "Should update investment product")]
        public async void ShouldUpdateInvestmentProduct()
        {
            // Arrange
            var investmentProduct = new InvestmentProductBuilder().Create();
            _investmentProductServiceFixture.SetupUpdate(investmentProduct);

            // Act
            var updatedInvestmentProduct = await _investmentProductService.Update(investmentProduct);

            // Assert
            Assert.NotNull(updatedInvestmentProduct);
            Assert.Equal(investmentProduct, updatedInvestmentProduct);
        }

        [Fact(DisplayName = "Should delete investment product")]
        public async void ShouldDeleteInvestmentProduct()
        {
            // Arrange
            var investmentProduct = new InvestmentProductBuilder().Create();
            _investmentProductServiceFixture.SetupDelete();

            // Act
            await _investmentProductService.Delete(investmentProduct);

            // Assert
            _investmentProductServiceFixture.Mocker.GetMock<IInvestmentProductRepository>()
                .Verify(x => x.DeleteAsync(It.IsAny<InvestmentProduct>()), Times.Once);
        }
    }
}
