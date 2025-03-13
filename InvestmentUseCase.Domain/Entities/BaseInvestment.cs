namespace InvestmentUseCase.Domain.Entities
{
    public abstract class BaseInvestment
    {
        public Guid Id { get; protected set; }
        public decimal InvestedCapital { get; protected set; }

        public BaseInvestment(decimal investedCapital)
        {
            Id = Guid.NewGuid();
            InvestedCapital = investedCapital;
        }


        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor de aporte deve ser maior que zero.");

            InvestedCapital += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor de resgate deve ser maior que zero.");

            if (amount > InvestedCapital)
                throw new InvalidOperationException("Não é possível resgatar mais do que o capital investido.");

            InvestedCapital -= amount;
        }
    }

}
