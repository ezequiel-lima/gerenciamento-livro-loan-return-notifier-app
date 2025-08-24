using GerenciamentoLivro.LoanReturnNotifierApp.Models;
using GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Services
{
    public class EmailNotificationService : INotificationService
    {
        private readonly ILogger _logger;

        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            _logger = logger;
        }

        public Task NotifyUserLoanAsync(UserLoanGroup userLoanGroup)
        {
            // TODO
            _logger.LogInformation("Sending e-mail to {UserName} (ID: {UserId})", userLoanGroup.UserName, userLoanGroup.UserId);
            foreach (var title in userLoanGroup.BookTitles)
            {
                _logger.LogInformation(" - {Book}", title);
            }

            return Task.CompletedTask;
        }
    }
}
