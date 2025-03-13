using InvestmentUseCase.Domain.Entities;

namespace InvestmentUseCase.Domain.Interfaces.Services
{
    public interface IInvestmentProductService
    {
        Task<InvestmentProduct> GetById(Guid id);
        Task<List<InvestmentProduct>> GetAll();
        Task<bool> Exists(Guid id);
        Task<bool> ExistsByCode(string code);
        Task<InvestmentProduct> Add(InvestmentProduct investmentProduct);
        Task<InvestmentProduct> Update(InvestmentProduct investmentProduct);
        Task Delete(InvestmentProduct investmentProduct);
    }
}
