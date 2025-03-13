using InvestmentUseCase.Domain.Interfaces.Notification;
using Microsoft.Extensions.Logging;

namespace InvestmentUseCase.Infra.Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendEmail(string email, string message)
        {
            _logger.LogInformation("Email enviado para {email} com a mensagem : {message}", email, message);

            Task.Delay(1000);

            _logger.LogInformation("Email enviado para {email} com a mensagem : {message}", email, message);

            return Task.CompletedTask;
        }
    }
}
