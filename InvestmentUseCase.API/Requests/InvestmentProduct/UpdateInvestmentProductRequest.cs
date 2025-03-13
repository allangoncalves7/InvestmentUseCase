using System.ComponentModel.DataAnnotations;

namespace InvestmentUseCase.API.Requests.InvestmentProduct
{
    public class UpdateInvestmentProductRequest
    {
        [Required(ErrorMessage = "Id é obrigatório")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Código é obrigatório")]
        [StringLength(10, ErrorMessage = "Código deve ter no máximo 10 caracteres")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Name { get; set; }
    }
}
