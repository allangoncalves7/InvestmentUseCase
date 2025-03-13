using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InvestmentUseCase.Infra.Data.Repositories
{
    public class InvestmentRepository : BaseRepository<Investment>, IInvestmentRepository
    {

        public InvestmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Investment> GetByCustomerAndProduct(string agency, string account, string dac, Guid investmentProductId)
        {
            var investments = await _context.Investments
                .Include(i => i.Customer)
                .Include(i => i.InvestmentProduct)
                .Where(i => i.Customer.Agency == agency &&
                            i.Customer.Account == account &&
                            i.Customer.DAC == dac &&
                            i.InvestmentProductId == investmentProductId)
                .FirstOrDefaultAsync();

            return investments;
        }

        public async Task<List<Investment>> GetAllByCustomer(string agency, string account, string dac)
        {
            var investments = await _context.Investments
                .Include(i => i.Customer)
                .Include(i => i.InvestmentProduct)
                .Where(i => i.Customer.Agency == agency &&
                            i.Customer.Account == account &&
                            i.Customer.DAC == dac)
                .ToListAsync();

            return investments;
        }
    }
}
