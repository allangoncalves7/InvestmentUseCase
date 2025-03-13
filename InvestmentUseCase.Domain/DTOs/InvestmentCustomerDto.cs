namespace InvestmentUseCase.Domain.DTOs
{
    public class InvestmentCustomerDto
    {
        public string CustomerName { get; set; }
        public string InvestmentProductName { get; set; }
        public string InvestmentProductCode { get; set; }
        public decimal InvestedCapital { get; set; }
    }
}
