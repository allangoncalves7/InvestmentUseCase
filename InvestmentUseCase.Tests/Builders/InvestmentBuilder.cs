using InvestmentUseCase.Domain.Entities;

namespace InvestmentUseCase.Tests.Builders
{
    public class InvestmentBuilder
    {
        private Guid _customerId = Guid.NewGuid();
        private Guid _investmentProductId = Guid.NewGuid();
        private decimal _investedCapital = 1000;


        public Investment Create(Guid id)
        {
            return new Investment(id, _customerId, _investmentProductId, _investedCapital);
        }

        public Investment Create()
        {
            return new Investment(_customerId, _investmentProductId, _investedCapital);
        }

        public Investment CreateWithCustomerAndProduct()
        {
            var investment = new Investment(_investmentProductId, _customerId, _investedCapital);
            var customer = new CustomerBuilder().Create(_customerId);
            var investmentProduct = new InvestmentProductBuilder().Create(_investmentProductId);

            typeof(Investment).GetProperty(nameof(Investment.Customer))?.SetValue(investment, customer);
            typeof(Investment).GetProperty(nameof(Investment.InvestmentProduct))?.SetValue(investment, investmentProduct);

            return investment;
        }

    }
}
