namespace InvestmentUseCase.Domain.Entities
{
    public class InvestmentProduct
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }

        private InvestmentProduct() { } 

        public InvestmentProduct(string name, string code)
        {
            Id = Guid.NewGuid();
            Name = name;
            Code = code;
        }

        public InvestmentProduct(Guid id,string code, string name)
        {
            Id = id;
            Name = name;
            Code = code;
        }

    }
}
