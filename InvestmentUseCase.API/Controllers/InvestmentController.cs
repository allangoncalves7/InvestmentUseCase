using InvestmentUseCase.API.Requests.Investment;
using InvestmentUseCase.API.Validations;
using InvestmentUseCase.Domain.DTOs;
using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvestmentUseCase.API.Controllers
{
    [ServiceFilter(typeof(ValidateModelAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentController(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }

        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Investment))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<Investment>> GetById(Guid id)
        {
            Investment investment = await _investmentService.GetById(id);

            if (investment is null)
                return NotFound("Investimento não encontrado!");

            return Ok(investment);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Investment>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<List<Investment>>> GetAll()
        {
            return Ok(await _investmentService.GetAll());
        }

        [HttpGet("GetByCustomerAndProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InvestmentCustomerDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<InvestmentCustomerDto>> GetByCustomerAndProduct([FromQuery] GetInvestmentByCustomerAndProductRequest request)
        {
            if (request is null)
                return BadRequest("Investimento não encontrado!");

            return Ok(await _investmentService.GetByCustomerAndProduct(request.Agency, request.Account, request.DAC, request.InvestmentProductId));
        }

        [HttpGet("GetByCustomer")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<InvestmentCustomerDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<List<InvestmentCustomerDto>>> GetByCustomer([FromQuery] GetInvestmentByCustomerRequest request)
        {
            return Ok(await _investmentService.GetAllByCustomer(request.Agency, request.Account, request.DAC));
        }

        [HttpPost("Add")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Investment))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<Investment>> Add(AddInvestmentRequest request)
        {
            bool exists = await _investmentService.ExistsByCustomer(request.CustomerId, request.InvestmentProductId);

            if (exists)
                return BadRequest("Investimento já cadastrado para o cliente!");

            Investment investment = new(request.InvestmentProductId, request.CustomerId, request.InvestedCapital);
            return Ok(await _investmentService.Add(investment));
        }

        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InvestmentCustomerDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult> Update(Guid id, UpdateInvestmentRequest request)
        {
            bool exists = await _investmentService.Exists(id);

            if (!exists)
                return NotFound("Investimento não encontrado!");

            Investment investment = new(id, request.InvestmentProductId, request.CustomerId, request.InvestedCapital);

            return Ok(await _investmentService.Update(investment));
        }

        [HttpPut("UpdateInvestedCapital")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Investment))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateInvestedCapital(Guid id, UpdateInvestedCapitalRequest request)
        {
            bool exists = await _investmentService.Exists(id);

            if (!exists)
                return NotFound("Investimento não encontrado!");

            return Ok(await _investmentService.UpdateInvestedCapital(id, request.Action, request.Amount));
        }

        [HttpDelete("Delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Delete(Guid id)
        {
            Investment investment = await _investmentService.GetById(id);

            if (investment is null)
                return NotFound("Investimento não encontrado!");

            await _investmentService.Delete(investment);

            return Ok("Investimento deletado com sucesso!");
        }
    }
}
