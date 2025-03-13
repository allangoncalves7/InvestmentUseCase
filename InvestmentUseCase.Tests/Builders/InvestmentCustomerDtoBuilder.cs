using InvestmentUseCase.Domain.DTOs;
using InvestmentUseCase.Domain.Entities;

namespace InvestmentUseCase.Tests.Builders
{
    public class InvestmentCustomerDtoBuilder
    {
        private string _customerName = "João Silva";
        private string _investmentProductName = "Tesouro Direto";
        private string _investmentProductCode = "TD";
        private decimal _investedCapital = 1000;

        public InvestmentCustomerDto Create()
        {
            return new InvestmentCustomerDto
            {
                CustomerName = _customerName,
                InvestmentProductName = _investmentProductName,
                InvestmentProductCode = _investmentProductCode,
                InvestedCapital = _investedCapital
            };
        }
    }
}
