using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Tests.IntegrationTests.Data;
using System.Net;
using System.Net.Http.Json;

namespace InvestmentUseCase.Tests.IntegrationTests.InvestmentProductController
{
    public class InvestmentProductControllerTests : IClassFixture<InvestmentApiFactory>
    {
        private readonly HttpClient _client;
        public InvestmentProductControllerTests(InvestmentApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Should return investment product when exists")]
        public async Task GetById_ShouldReturnInvestmentProduct_WhenExists()
        {
            // Arrange
            var id = Guid.Parse("cd939f0a-fc4b-423c-b001-f7a558ba483d");

            // Act
            var response = await _client.GetAsync($"/api/InvestmentProduct/GetById?id={id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should return NotFound when investment product does not exist")]
        public async Task GetById_ShouldReturnNotFound_WhenInvestmentProdudctDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync($"/api/InvestmentProduct/GetById?id={Guid.NewGuid()}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Should return Ok when get all investment products")]
        public async Task GetAll_ShouldReturnsOk()
        {
            // Act
            var response = await _client.GetAsync($"/api/InvestmentProduct/GetAll");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should create investment product when data is valid")]
        public async Task Add_ShouldCreateInvestmentProdudct_WhenDataIsValid()
        {
            // Arrange
            var request = new
            {
                Id = Guid.NewGuid(),
                Code = "TD0012",
                Name = "Tesouro Direto"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/InvestmentProduct/Add", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when data is invalid")]
        public async Task Add_ShouldReturnBadRequest_WhenDataIsInvalid()
        {
            // Arrange
            var request = new
            {
                Code = "TD0012",
                Name = ""
            };
            // Act
            var response = await _client.PostAsJsonAsync("/api/InvestmentProduct/Add", request);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Should return Ok when update investment product")]
        public async Task Update_ShouldReturnOk_WhenUpdateInvestmentProduct()
        {
            // Arrange
            var id = Guid.Parse("cd939f0a-fc4b-423c-b001-f7a558ba483d");
            var request = new
            {
                Id = id,
                Code = "TD0012",
                Name = "Tesouro Direto"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/InvestmentProduct/Update?id={request.Id}", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when update investment product with invalid data")]
        public async Task Update_ShouldReturnBadRequest_WhenUpdateInvestmentProductWithInvalidData()
        {
            // Arrange
            var id = Guid.Parse("cd939f0a-fc4b-423c-b001-f7a558ba483d");
            var request = new
            {
                Id = id,
                Code = "TD0012",
                Name = ""
            };
            // Act
            var response = await _client.PutAsJsonAsync($"/api/InvestmentProduct/Update?id={request.Id}", request);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Should return Ok when delete investment product")]
        public async Task Delete_ShouldRemoveCustomer_WhenCustomerExists()
        {
            // Arrange
            var id = Guid.Parse("0303d605-a32a-4587-b641-160f008e5d14");

            // Act
            var response = await _client.DeleteAsync($"/api/InvestmentProduct/Delete?id={id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
