using System.ComponentModel.DataAnnotations;

namespace InvestmentUseCase.API.Requests.Investment
{
    public class GetInvestmentByCustomerAndProductRequest
    {
        [Required(ErrorMessage = "Id do produto de investimento é obrigatório")]
        public Guid InvestmentProductId { get; set; }
        [Required(ErrorMessage = "A Agência é obrigatória!")]
        [StringLength(10, ErrorMessage = "A Agência deve ter no máximo 10 caracteres")]
        public string Agency { get; set; }
        [Required(ErrorMessage = "A Conta é obrigatória!")]
        [StringLength(10, ErrorMessage = "A Conta deve ter no máximo 10 caracteres")]
        public string Account { get; set; }
        [Required(ErrorMessage = "O DAC é obrigatório!")]
        [StringLength(1, ErrorMessage = "O DAC deve ter 1 caractere")]
        public string DAC { get; set; }
    }
}
