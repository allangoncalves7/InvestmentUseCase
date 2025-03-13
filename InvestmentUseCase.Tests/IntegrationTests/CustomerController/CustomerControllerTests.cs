using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Tests.IntegrationTests.Data;
using System.Net;
using System.Net.Http.Json;

namespace InvestmentUseCase.Tests.IntegrationTests.CustomerController
{
    public class CustomerControllerTests : IClassFixture<InvestmentApiFactory>
    {
        private readonly HttpClient _client;
        public CustomerControllerTests(InvestmentApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Should return customer when exists")]
        public async Task GetById_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            var id = Guid.Parse("5dfa53c1-973f-41d4-9366-cbbb2922b22e");

            // Act
            var response = await _client.GetAsync($"/api/Customer/GetById?id={id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should return NotFound when investment does not exist")]
        public async Task GetById_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync($"/api/Customer/GetById?id={Guid.NewGuid()}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Should return Ok when get all customers")]
        public async Task GetAll_ShouldReturnsOk()
        {
            // Act
            var response = await _client.GetAsync($"/api/Customer/GetAll");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should create customer when data is valid")]
        public async Task Add_ShouldCreateCustomer_WhenDataIsValid()
        {
            // Arrange
            var request = new
            {
                Name = "Novo Cliente",
                Email = "novo@email.com",
                Agency = "1234",
                Account = "56789",
                DAC = "0"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Customer/Add", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when data is invalid")]
        public async Task Add_ShouldReturnBadRequest_WhenDataIsInvalid()
        {
            // Arrange
            var request = new
            {
                Name = "",
                Email = "novoemail.com",
                Agency = "1234",
                Account = "56789",
                DAC = "0"
            };
            // Act
            var response = await _client.PostAsJsonAsync("/api/Customer/Add", request);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Should return Ok when update investment")]
        public async Task Update_ShouldReturnOk_WhenUpdateCustomer()
        {
            // Arrange
            var id = Guid.Parse("5dfa53c1-973f-41d4-9366-cbbb2922b22e");

            var request = new
            {
                Id = id,
                Name = "Cliente",
                Email = "novo@email.com",
                Agency = "1234",
                Account = "56789",
                DAC = "1"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/Customer/Update?id={request.Id}", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when update investment with invalid data")]
        public async Task Update_ShouldReturnBadRequest_WhenUpdateCustomerWithInvalidData()
        {
            // Arrange
            var id = Guid.Parse("5dfa53c1-973f-41d4-9366-cbbb2922b22e");
            var request = new
            {
                Id = id,
                Name = "",
                Email = "novoemail.com",
                Agency = "1234",
                Account = "56789",
                DAC = "0"
            };
            // Act
            var response = await _client.PutAsJsonAsync($"/api/Customer/Update?id={request.Id}", request);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Should return Ok when delete customer")]
        public async Task Delete_ShouldRemoveCustomer_WhenCustomerExists()
        {
            // Arrange
            var id = Guid.Parse("16aecf48-db83-479f-9556-913414c407bf");

            // Act
            var response = await _client.DeleteAsync($"/api/Customer/Delete?id={id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }


}
