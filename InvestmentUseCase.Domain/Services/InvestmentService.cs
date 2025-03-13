using InvestmentUseCase.Domain.DTOs;
using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Enums;
using InvestmentUseCase.Domain.Interfaces.Notification;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Interfaces.Services;
using InvestmentUseCase.Domain.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InvestmentUseCase.Domain.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly ILogger<InvestmentService> _logger;
        private readonly IInvestmentRepository _investmentRepository;
        private readonly INotificationService _notificationService;

        public InvestmentService(ILogger<InvestmentService> logger,
            IInvestmentRepository investmentRepository,
            INotificationService notificationService)
        {
            _logger = logger;
            _investmentRepository = investmentRepository;
            _notificationService = notificationService;
        }

        public async Task<Investment> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation("Buscando investmento pelo Id {Id}", id);
                return await _investmentRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro a buscar investimento Id {Id}", id);
                throw;
            }
        }

        public async Task<List<Investment>> GetAll()
        {
            try
            {
                _logger.LogInformation("Buscando todos investmentos");
                return await _investmentRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar investimentos");
                throw;
            }
        }

        public async Task<bool> Exists(Guid id)
        {
            try
            {
                _logger.LogInformation("Verificando se o investimento existe pelo Id {Id}", id);
                return await _investmentRepository.ExistsAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se o investimento existe pelo Id {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsByCustomer(Guid customerId, Guid investmentProductId)
        {
            try
            {
                _logger.LogInformation("Verificando se o investimento existe pelo Id do Cliente: {CustomerID} e Id do Produto de Investimento {InvestmentProductId}"
                    , customerId, investmentProductId);

                return await _investmentRepository.ExistsAsync(i => i.CustomerId == customerId && 
                    i.InvestmentProductId == investmentProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se o investimento existe ppelo Id do Cliente: {CustomerID} e Id do Produto de Investimento {InvestmentProductId}"
                    , customerId, investmentProductId);
                throw;
            }
        }

        public async Task<InvestmentCustomerDto> GetByCustomerAndProduct(string agency, string account, string dac, Guid investmentProductId)
        {
            try
            {
                _logger.LogInformation("Buscando investimentos do cliente pela Agência: {Agency}, Conta: {Account} e DAC: {DAC}", agency, account, dac);

                Investment investment = await _investmentRepository.GetByCustomerAndProduct(agency, account, dac, investmentProductId);

                if (investment is null)
                    return null;

                return new InvestmentCustomerDto
                {
                    CustomerName = investment.Customer.Name,
                    InvestmentProductName = investment.InvestmentProduct.Name,
                    InvestmentProductCode = investment.InvestmentProduct.Code,
                    InvestedCapital = investment.InvestedCapital
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar investimentos do cliente pela Agência: {Agency}, Conta: {Account} e DAC: {DAC}", agency, account, dac);
                throw;
            }
        }

        public async Task<List<InvestmentCustomerDto>> GetAllByCustomer(string agency, string account, string dac)
        {
            try
            {
                _logger.LogInformation("Buscando investimentos do cliente pela Agência: {Agency}, Conta: {Account} e DAC: {DAC}", agency, account, dac);

                List<Investment> investments = await _investmentRepository.GetAllByCustomer(agency, account, dac);

                return [.. investments.Select(i => new InvestmentCustomerDto
                {
                    CustomerName = i.Customer.Name,
                    InvestmentProductName = i.InvestmentProduct.Name,
                    InvestmentProductCode = i.InvestmentProduct.Code,
                    InvestedCapital = i.InvestedCapital
                })];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar investimentos do cliente pela Agência: {Agency}, Conta: {Account} e DAC: {DAC}", agency, account, dac);
                throw;
            }
        }

        public async Task<Investment> Add(Investment investment)
        {
            try
            {

                _logger.LogInformation("Adicionando investimento {Investment}", JsonUtils.Serialize(investment));
                await _investmentRepository.AddAsync(investment);

                _logger.LogInformation("Buscando informações complementares do Investimento pelo Id {Id}", investment.Id);
                Investment investmentAdd = _investmentRepository.Include(i => i.Customer, i => i.InvestmentProduct)
                    .FirstOrDefault(i => i.Id == investment.Id);

                _logger.LogInformation("Enviando notificação de investimento realizado para o cliente {Customer}. Investimento {Investment}", investment.Customer.Name, JsonUtils.Serialize(investment));
                await _notificationService.SendEmail(investmentAdd.Customer.Email, $"Investimento realizado no {investmentAdd.InvestmentProduct.Name}. Investimento de {investment.InvestedCapital} realizado com sucesso");

                return investment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar investimento {Investment}", JsonUtils.Serialize(investment));
                throw;
            }
        }

        public async Task<Investment> Update(Investment investment)
        {
            try
            {
                _logger.LogInformation("Atualizando investimento {Investment}", JsonUtils.Serialize(investment));
                return await _investmentRepository.UpdateAsync(investment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar investimento {Investment}", JsonUtils.Serialize(investment));
                throw;
            }
        }

        public async Task<Investment> UpdateInvestedCapital(Guid id, EInvestmentAction action, decimal amount)
        {
            try
            {

                _logger.LogInformation("Buscando informações complementares do Investimento pelo Id {Id}", id);
                Investment investment = _investmentRepository.Include(i => i.Customer, i => i.InvestmentProduct)
                   .FirstOrDefault(i => i.Id == id);

                if (action == EInvestmentAction.Deposit)
                {
                    _logger.LogInformation("Depositando valor de {Amount} no investimento {Investment}", amount, investment);
                    investment.Deposit(amount);
                }
                else
                {
                    _logger.LogInformation("Sacando valor de {Amount} no investimento {Investment}", amount, investment);
                    investment.Withdraw(amount);
                }

                _logger.LogInformation("Atualizando capital investido do investimento pelo Id {Id}", id);
                await _investmentRepository.UpdateAsync(investment);

                _logger.LogInformation("Enviando notificação de {Action} no investmento {Investment}", action, JsonUtils.Serialize(investment));
                await _notificationService.SendEmail(investment.Customer.Email, $"Você fez um {action.GetDescription()} no {investment.InvestmentProduct.Name}. Investimento de {investment.InvestedCapital} consluído com sucesso");


                return investment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar capital investido do investimento pelo Id {Id}", id);
                throw;
            }
        }

        public async Task Delete(Investment investment)
        {
            try
            {
                _logger.LogInformation("Deletando investimento {Investment}", JsonUtils.Serialize(investment));
                await _investmentRepository.DeleteAsync(investment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar investimento {Investment}", JsonUtils.Serialize(investment));
                throw;
            }
        }
    }
}
