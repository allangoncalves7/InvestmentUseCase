using InvestmentUseCase.Domain.Entities;

namespace InvestmentUseCase.Domain.Interfaces.Repositories
{
    public interface IInvestmentRepository : IBaseRepository<Investment>
    {
        Task<Investment> GetByCustomerAndProduct(string agency, string account, string dac, Guid investmentProductId);
        Task<List<Investment>> GetAllByCustomer(string agency, string account, string dac);
    }
}
