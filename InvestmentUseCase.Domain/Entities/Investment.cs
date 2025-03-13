namespace InvestmentUseCase.Domain.Entities
{
    public class Investment : BaseInvestment
    {
        public Guid InvestmentProductId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public InvestmentProduct InvestmentProduct { get; private set; }

        private const decimal Tax = 0.1m;

        private Investment(): base(0) { }

        public Investment(Guid investmentProductId, Guid customerId, decimal investedCapital)
        : base(investedCapital)
        {
            Id = Guid.NewGuid();
            InvestmentProductId = investmentProductId;
            CustomerId = customerId;
        }

        public Investment(Guid investmentId, Guid investmentProductId, Guid customerId, decimal investedCapital)
        : base(investedCapital)
        {
            Id = investmentId;
            InvestmentProductId = investmentProductId;
            CustomerId = customerId;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor do resgate deve ser maior que zero.");

            if (amount > InvestedCapital)
                throw new InvalidOperationException("Não é possível resgatar mais do que o capital investido.");

            var fee = amount * Tax;
            var totalAmount = amount + fee;

            if (totalAmount > InvestedCapital)
                throw new InvalidOperationException("O valor do resgate somado à taxa não pode ser maior que o saldo.");

            InvestedCapital -= totalAmount;
        }
    }
}
