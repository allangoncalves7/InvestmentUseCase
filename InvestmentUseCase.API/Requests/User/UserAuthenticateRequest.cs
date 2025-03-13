using System.ComponentModel.DataAnnotations;

namespace InvestmentUseCase.API.Requests.User
{
    public class UserAuthenticateRequest
    {
        [Required(ErrorMessage = "Usuário é obrigatório")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Password { get; set; }
    }
}
