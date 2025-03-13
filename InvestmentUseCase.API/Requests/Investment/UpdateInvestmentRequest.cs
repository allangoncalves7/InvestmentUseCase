using System.ComponentModel.DataAnnotations;

namespace InvestmentUseCase.API.Requests.Investment
{
    public class UpdateInvestmentRequest
    {
        [Required(ErrorMessage = "Id é obrigatório")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Capital investido é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Capital investido deve ser maior que 0")]
        public decimal InvestedCapital { get; set; }
        [Required(ErrorMessage = "Id do produto de investimento é obrigatório")]
        public Guid InvestmentProductId { get; set; }
        [Required(ErrorMessage = "Id do cliente é obrigatório")]
        public Guid CustomerId { get; set; }
    }
}
