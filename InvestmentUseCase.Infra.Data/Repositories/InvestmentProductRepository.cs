using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Infra.Data.Context;

namespace InvestmentUseCase.Infra.Data.Repositories
{
    public class InvestmentProductRepository : BaseRepository<InvestmentProduct>, IInvestmentProductRepository
    {

        public InvestmentProductRepository(AppDbContext context) : base(context)
        {
        }

    }
}
