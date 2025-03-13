namespace InvestmentUseCase.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Agency { get; private set; }
        public string Account { get; private set; }
        public string DAC { get; private set; }

        private readonly List<Investment> _investments = [];
        public IReadOnlyCollection<Investment> Investments => _investments.AsReadOnly();

        private Customer() { }

        public Customer(string name, string email, string agency, string account, string dac)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Agency = agency;
            Account = account;
            DAC = dac;
        }

        public Customer(Guid id, string name, string email, string agency, string account, string dac)
        {
            Id = id;
            Name = name;
            Email = email;
            Agency = agency;
            Account = account;
            DAC = dac;
        }
    }
}
