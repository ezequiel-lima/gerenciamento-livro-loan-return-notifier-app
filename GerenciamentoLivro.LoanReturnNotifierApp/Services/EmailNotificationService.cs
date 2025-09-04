using GerenciamentoLivro.LoanReturnNotifierApp.HttpClients;
using GerenciamentoLivro.LoanReturnNotifierApp.Models;
using GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly ILogger _logger;
        private readonly IEmailHttpClient _emailHttpClient;

        public EmailNotificationService(ILoggerFactory loggerFactory, IEmailHttpClient emailHttpClient)
        {
            _logger = loggerFactory.CreateLogger<EmailNotificationService>();
            _emailHttpClient = emailHttpClient;
        }

        public async Task NotifyUserLoanAsync(UserModel userModel)
        {
            _logger.LogInformation("Preparing to send email to {UserName} <{UserEmail}>", userModel.UserName, userModel.UserEmail);

            var subject = "Reminder: You have overdue books";
            var html = BuildHtmlBody(userModel);
            var text = BuildTextBody(userModel);

            var success = await _emailHttpClient.SendEmailAsync(
                toEmail: userModel.UserEmail,
                toName: userModel.UserName,
                subject: subject,
                html: html,
                text: text
            );
        }

        private string BuildHtmlBody(UserModel user)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"<p>Hello <strong>{user.UserName}</strong>,</p>");
            stringBuilder.Append("<p>You have the following overdue books:</p><ul>");

            foreach (var title in user.BookTitles)
            {
                stringBuilder.Append($"<li>{title}</li>");
            }

            stringBuilder.Append("</ul><p>Please resolve this situation as soon as possible.</p>");
            return stringBuilder.ToString();
        }

        private string BuildTextBody(UserModel user)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Hello {user.UserName},");
            stringBuilder.AppendLine("You have the following overdue books:");

            foreach (var title in user.BookTitles)
            {
                stringBuilder.AppendLine($"- {title}");
            }

            stringBuilder.AppendLine("Please resolve this situation as soon as possible.");
            return stringBuilder.ToString();
        }
    }
}
