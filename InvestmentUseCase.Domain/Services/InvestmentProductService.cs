using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Interfaces.Services;
using InvestmentUseCase.Domain.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InvestmentUseCase.Domain.Services
{
    public class InvestmentProductService : IInvestmentProductService
    {
        private readonly ILogger<InvestmentProductService> _logger;
        private readonly IInvestmentProductRepository _investmentProductRepository;

        public InvestmentProductService(ILogger<InvestmentProductService> logger, IInvestmentProductRepository investmentProductRepository)
        {
            _logger = logger;
            _investmentProductRepository  = investmentProductRepository ;
        }

        public async Task<InvestmentProduct> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation("Buscando produto de investimento pelo Id {Id}", id);
                return await _investmentProductRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produto de investimento pelo Id {Id}", id);
                throw;
            }
        }

        public async Task<List<InvestmentProduct>> GetAll()
        {
            try
            {
                _logger.LogInformation("Buscando todos os produtos de investimento");
                return await _investmentProductRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os produtos de investimento");
                throw;
            }
        }

        public async Task<bool> Exists(Guid id)
        {
            try
            {
                _logger.LogInformation("Verificando se produto de investimento existe pelo Id {Id}", id);
                return await _investmentProductRepository.ExistsAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se produto de investimento existe pelo Id {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsByCode(string code)
        {
            try
            {
                _logger.LogInformation("Verificando se produto de investimento existe pelo Código {Code}", code);
                return await _investmentProductRepository.ExistsAsync(i => i.Code == code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se produto de investimento existe pelo Código {Code}", code);
                throw;
            }
        }

        public async Task<InvestmentProduct> Add(InvestmentProduct investmentProduct)
        {
            try
            {
                _logger.LogInformation("Adicionando Produto de Investimento {InvestmentProduct}", JsonUtils.Serialize(investmentProduct));
                return await _investmentProductRepository.AddAsync(investmentProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar Produto de Investimento {InvestmentProduct}", JsonUtils.Serialize(investmentProduct));
                throw;
            }
        }

        public async Task<InvestmentProduct> Update(InvestmentProduct investmentProduct)
        {
            try
            {
                _logger.LogInformation("Atualizando Produto de Investimento {InvestmentProduct}", JsonUtils.Serialize(investmentProduct));
                return await _investmentProductRepository.UpdateAsync(investmentProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar Produto de Investimento {InvestmentProduct}", JsonUtils.Serialize(investmentProduct));
                throw;
            }
        }

        public async Task Delete(InvestmentProduct investmentProduct)
        {
            try
            {
                _logger.LogInformation("Deletando Produto de Investimento {InvestmentProduct}", JsonUtils.Serialize(investmentProduct));
                await _investmentProductRepository.DeleteAsync(investmentProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar Produto de Investimento {InvestmentProduct}", JsonUtils.Serialize(investmentProduct));
                throw;
            }
        }
    }
}
