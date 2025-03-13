using InvestmentUseCase.Domain.Entities;

namespace InvestmentUseCase.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetById(Guid id);
        Task<List<Customer>> GetAll();
        Task<bool> Exists(Guid id);
        Task<bool> ExistsByAccount(string agency, string account, string dac);
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(Customer customer);
        Task Delete(Customer customer);
    }
}
