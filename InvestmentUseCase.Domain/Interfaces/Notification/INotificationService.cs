namespace InvestmentUseCase.Domain.Interfaces.Notification
{
    public interface INotificationService
    {
        Task SendEmail(string email, string message);
    }
}
