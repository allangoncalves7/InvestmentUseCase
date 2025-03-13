using System.ComponentModel.DataAnnotations;

namespace InvestmentUseCase.API.Requests.Customer
{
    public class AddCustomerRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email é obrigatório")]
        [StringLength(200, ErrorMessage = "Email deve ter no máximo 200 caracteres")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Agência é obrigatória")]
        [StringLength(10, ErrorMessage = "Agência deve ter no máximo 10 caracteres")]
        public string Agency { get; set; }
        [Required(ErrorMessage = "Conta é obrigatória")]
        [StringLength(10, ErrorMessage = "Conta deve ter no máximo 10 caracteres")]
        public string Account { get; set; }
        [Required(ErrorMessage = "DAC é obrigatório")]
        [StringLength(1, ErrorMessage = "DAC deve ter 1 caractere")]
        public string DAC { get; set; }
    }
}
