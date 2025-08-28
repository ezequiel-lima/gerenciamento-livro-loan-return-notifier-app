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

        public Task NotifyUserLoanAsync(UserModel userModel)
        {
            // TODO
            _logger.LogInformation("Sending e-mail to {UserName} (ID: {UserId})", userModel.UserName, userModel.UserId);
            foreach (var title in userModel.BookTitles)
            {
                _logger.LogInformation(" - {Book}", title);
            }

            return Task.CompletedTask;
        }
    }
}
