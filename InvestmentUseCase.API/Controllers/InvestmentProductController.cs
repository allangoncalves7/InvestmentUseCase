using InvestmentUseCase.API.Requests.InvestmentProduct;
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
    public class InvestmentProductController : ControllerBase
    {
        private readonly IInvestmentProductService _investmentProductService;

        public InvestmentProductController(IInvestmentProductService investmentProductService)
        {
            _investmentProductService = investmentProductService;
        }

        [HttpGet("GetById")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InvestmentProduct))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<InvestmentProduct>> GetById(Guid id)
        {
            InvestmentProduct investmentProduct = await _investmentProductService.GetById(id);

            if (investmentProduct is null)
                return NotFound("Produto de investimento não encontrado!");
            return Ok(investmentProduct);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<InvestmentProduct>))]
        public async Task<ActionResult<List<InvestmentProduct>>> GetAll()
        {
            return Ok(await _investmentProductService.GetAll());
        }

        [HttpPost("Add")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InvestmentProduct))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<InvestmentProduct>> Add(AddInvestmentProductRequest request)
        {
            bool exists = await _investmentProductService.ExistsByCode(request.Code);
            if (exists)
                return BadRequest("Código do produto de investimento já cadastrado!");

            InvestmentProduct investmentProduct = new(request.Name, request.Code);
            return Ok(await _investmentProductService.Add(investmentProduct));
        }

        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InvestmentProduct))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<InvestmentProduct>> Update(Guid id, UpdateInvestmentProductRequest request)
        {
            bool exists = await _investmentProductService.Exists(id);

            if (!exists)
                return NotFound("Produto de investimento não encontrado!");

            InvestmentProduct investmentProduct = new(id, request.Name, request.Code);

            return Ok(await _investmentProductService.Update(investmentProduct));
        }

        [HttpDelete("Delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult> Delete(Guid id)
        {
            InvestmentProduct investmentProduct = await _investmentProductService.GetById(id);

            if (investmentProduct is null)
                return NotFound("Produto de investimento não encontrado!");

            await _investmentProductService.Delete(investmentProduct);

            return Ok("Produto de investimento deletado com sucesso!");
        }


    }
}
