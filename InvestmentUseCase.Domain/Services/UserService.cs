using InvestmentUseCase.Domain.DTOs;
using InvestmentUseCase.Domain.Interfaces.Auth;
using InvestmentUseCase.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace InvestmentUseCase.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IJwtService _jwtService;

        public UserService(ILogger<UserService> logger, IJwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        public UserAuthenticateDto Authenticate(string username, string password)
        {
            try
            {
                if (username != "admin" && password != "admin")
                {
                    _logger.LogWarning("Usuário {User} não autenticado. Usuário ou senha inválidos", username);
                    return null;
                }

                _logger.LogInformation("Usuário {User} autenticado. Gerando token de acesso;", username);
                return new UserAuthenticateDto
                {
                    AccessToken = _jwtService.GenerateToken(Guid.NewGuid(), username)
                };  

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao autenticar usuário {User}", username);
                throw;
            }
           
        }
    }
}
