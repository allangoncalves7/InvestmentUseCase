namespace InvestmentUseCase.Domain.Interfaces.Auth
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string username);
    }
}
