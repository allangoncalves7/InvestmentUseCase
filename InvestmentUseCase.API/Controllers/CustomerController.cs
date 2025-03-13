using InvestmentUseCase.API.Requests.Customer;
using InvestmentUseCase.API.Validations;
using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvestmentUseCase.API.Controllers
{
    [ServiceFilter(typeof(ValidateModelAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Customer))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<Customer>> GetById(Guid id)
        {
            Customer customer = await _customerService.GetById(id);

            if (customer is null)
                return NotFound("Cliente não encontrado!");

            return Ok(customer);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Customer>))]
        public async Task<ActionResult<List<Customer>>> GetAll()
        {
            return Ok(await _customerService.GetAll());
        }

        [HttpPost("Add")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Customer))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<Customer>> Add(AddCustomerRequest request)
        {
            bool exists = await _customerService.ExistsByAccount(request.Agency, request.Account, request.DAC);

            if (exists)
                return BadRequest("Já existe um cliente com esta conta!");

            Customer customer = new(request.Name, request.Email, request.Agency, request.Account, request.DAC) ;

            return Ok(await _customerService.Add(customer));
        }

        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Customer))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<Customer>> Update(Guid id, UpdateCustomerRequest request)
        {
            bool exists = await _customerService.Exists(id);

            if (!exists)
                return NotFound("Ciente não encontrado!");

            Customer customer = new(id, request.Name, request.Email, request.Agency, request.Account, request.DAC);
            return Ok(await _customerService.Update(customer));
        }

        [HttpDelete("Delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult> Delete(Guid id)
        {
            Customer customer = await _customerService.GetById(id);

            if (customer is null)
                return NotFound("Cliente não encontrado!");

            await _customerService.Delete(customer);

            return Ok("Cliente deletado com sucesso!");
        }
    }
}
