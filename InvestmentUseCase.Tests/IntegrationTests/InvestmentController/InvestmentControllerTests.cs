using InvestmentUseCase.Tests.IntegrationTests.Data;
using System.Net;
using System.Net.Http.Json;

namespace InvestmentUseCase.Tests.IntegrationTests.InvestmentController
{
    public class InvestmentControllerTests : IClassFixture<InvestmentApiFactory>
    {
        private readonly HttpClient _client;
        public InvestmentControllerTests(InvestmentApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Should return investment when exists")]
        public async Task GetById_ShouldReturnInvestment_WhenExists()
        {
            // Arrange
            var id = Guid.Parse("bff7a047-04d6-4cd0-9724-f316fc958f68");
            // Act
            var response = await _client.GetAsync($"/api/Investment/GetById?id={id}");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should return NotFound when investment does not exist")]
        public async Task GetById_ShouldReturnNotFound_WhenInvestmentDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync($"/api/Investment/GetById?id={Guid.NewGuid()}");
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Should return Ok when get all investments")]
        public async Task GetAll_ShouldReturnsOk()
        {
            // Act
            var response = await _client.GetAsync($"/api/Investment/GetAll");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should create investment when data is valid")]
        public async Task Add_ShouldCreateInvestment_WhenDataIsValid()
        {
            // Arrange
            var request = new
            {
                InvestmentProductId = Guid.Parse("cd939f0a-fc4b-423c-b001-f7a558ba483d"),
                CustomerId = Guid.Parse("16aecf48-db83-479f-9556-913414c407bf"),
                InvestedCapital = 1000.00m
            };
            // Act
            var response = await _client.PostAsJsonAsync("/api/Investment/Add", request);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when data is invalid")]
        public async Task Add_ShouldReturnBadRequest_WhenDataIsInvalid()
        {
            // Arrange
            var request = new
            {
                ProductId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Value = 0.00m
            };
            // Act
            var response = await _client.PostAsJsonAsync("/api/Investment/Add", request);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Should return Ok when update investment")]
        public async Task Update_ShouldReturnOk_WhenUpdateInvestment()
        {
            // Arrange
            var request = new
            {
                Id = Guid.Parse("bff7a047-04d6-4cd0-9724-f316fc958f68"),
                InvestmentProductId = Guid.Parse("cd939f0a-fc4b-423c-b001-f7a558ba483d"),
                CustomerId = Guid.Parse("5dfa53c1-973f-41d4-9366-cbbb2922b22e"),
                InvestedCapital = 2000.00m
            };
            // Act
            var response = await _client.PutAsJsonAsync($"/api/Investment/Update?id={request.Id}", request);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when update investment with invalid data")]
        public async Task Update_ShouldReturnBadRequest_WhenUpdateInvestmentWithInvalidData()
        {
            // Arrange
            var request = new
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                InvestedCapital = 0.00m
            };
            // Act
            var response = await _client.PutAsJsonAsync("/api/Investment/Update", request);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Should return Ok when delete investment")]
        public async Task Delete_ShouldReturnOk_WhenDeleteInvestment()
        {
            // Arrange
            var id = Guid.Parse("642161a6-92d1-4df9-99e8-8f2a99bcf458");
            // Act
            var response = await _client.DeleteAsync($"/api/Investment/Delete?id={id}");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
