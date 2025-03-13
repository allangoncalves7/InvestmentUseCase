using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Interfaces.Services;
using InvestmentUseCase.Domain.Utils;
using Microsoft.Extensions.Logging;

namespace InvestmentUseCase.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation("Buscando cliente pelo Id {Id}", id);
                return await _customerRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar cliente pelo Id {Id}", id);
                throw;
            }
        }

        public async Task<List<Customer>> GetAll()
        {
            try
            {
                _logger.LogInformation("Buscando todos os clientes");
                return await _customerRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar clientes");
                throw;
            }
        }

        public async Task<bool> Exists(Guid id)
        {
            try
            {
                _logger.LogInformation("Verificando se cliente existe pelo Id {Id}", id);
                return await _customerRepository.ExistsAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Erro ao verificar se cliente existe pelo Id {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsByAccount(string agency, string account, string dac)
        {
            try
            {
                _logger.LogInformation("Verificando se já existe a conta pela Agência {Agency}, Conta {Account} e DAC {Dac}", agency, account, dac);
                return await _customerRepository.ExistsAsync(c => c.Agency == agency && c.Account == account && c.DAC == dac);
            }
            catch (Exception ex)
            {
                string message = $"Erro ao verificar se já existe a conta pela Agência {agency}, Conta {account} e DAC {dac}";
                _logger.LogError(ex,"{Message}", message);
                throw new Exception(message);
            }
        }

        public async Task<Customer> Add(Customer customer)
        {
            try
            {
                _logger.LogInformation("Adicionando cliente {Customer}", JsonUtils.Serialize(customer));
                return await _customerRepository.AddAsync(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar cliente {Customer}", JsonUtils.Serialize(customer));
                throw;
            }
        }

        public async Task<Customer> Update(Customer customer)
        {
            try
            {
                _logger.LogInformation("Atualizando cliente {Customer}", JsonUtils.Serialize(customer));
                return await _customerRepository.UpdateAsync(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar cliente {Customer}", JsonUtils.Serialize(customer));
                throw;
            }
        }

        public async Task Delete(Customer customer)
        {
            try
            {
                _logger.LogInformation("Deletando cliente {Customer}", JsonUtils.Serialize(customer));
                await _customerRepository.DeleteAsync(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar cliente {Customer}", JsonUtils.Serialize(customer));
                throw;
            }
        }
    }
}
