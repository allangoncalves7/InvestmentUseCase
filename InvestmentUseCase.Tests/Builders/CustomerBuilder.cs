using InvestmentUseCase.Domain.Entities;

namespace InvestmentUseCase.Tests.Builders
{
    public class CustomerBuilder
    {
        private string _name = "João Silva";
        private string _email = "joao@email.com";
        private string _agency = "1234";
        private string _account = "56789";
        private string _dac = "1";

        public Customer Create(Guid id)
        {
            return new Customer(id, _name, _email, _agency, _account, _dac);
        }

        public Customer Create()
        {
            return new Customer(_name, _email, _agency, _account, _dac);
        }
    }
}
