using InvestmentUseCase.Domain.DTOs;

namespace InvestmentUseCase.Domain.Interfaces.Services
{
    public interface IUserService
    {
        UserAuthenticateDto Authenticate(string username, string password);
    }
}
