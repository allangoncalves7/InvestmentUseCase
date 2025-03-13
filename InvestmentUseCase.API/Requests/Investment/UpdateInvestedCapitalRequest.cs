using InvestmentUseCase.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace InvestmentUseCase.API.Requests.Investment
{
    public class UpdateInvestedCapitalRequest
    {
        [Required(ErrorMessage = "É obritatório informar se é depósito ou resgate")]
        public EInvestmentAction Action { get; set; }
        [Required(ErrorMessage = "É obrigatório informar o valor")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Capital investido deve ser maior que 0")]
        public decimal Amount { get; set; }
    }
}
