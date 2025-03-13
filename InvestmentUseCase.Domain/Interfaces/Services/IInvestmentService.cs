using InvestmentUseCase.Domain.DTOs;
using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Enums;

namespace InvestmentUseCase.Domain.Interfaces.Services
{
    public interface IInvestmentService
    {
        Task<Investment> GetById(Guid id);
        Task<List<Investment>> GetAll();
        Task<bool> Exists(Guid id);
        Task<bool> ExistsByCustomer(Guid customerId, Guid investmentProductId);
        Task<InvestmentCustomerDto> GetByCustomerAndProduct(string agency, string account, string dac, Guid investmentProductId);
        Task<List<InvestmentCustomerDto>> GetAllByCustomer(string agency, string account, string dac);
        Task<Investment> Add(Investment investment);
        Task<Investment> Update(Investment investment);
        Task<Investment> UpdateInvestedCapital(Guid id, EInvestmentAction action, decimal amount);
        Task Delete(Investment investment);
    }
}
