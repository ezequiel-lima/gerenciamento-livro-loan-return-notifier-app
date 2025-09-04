using GerenciamentoLivro.LoanReturnNotifierApp.Configurations;
using GerenciamentoLivro.LoanReturnNotifierApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GerenciamentoLivro.LoanReturnNotifierApp.HttpClients
{
    public class EmailHttpClient : IEmailHttpClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly EmailApiSettings _emailApiSettings;

        public EmailHttpClient(ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory, IOptions<EmailApiSettings> emailApiSettings)
        {
            _logger = loggerFactory.CreateLogger<EmailHttpClient>();
            _httpClient = httpClientFactory.CreateClient();
            _emailApiSettings = emailApiSettings.Value;

            _httpClient.BaseAddress = new Uri(_emailApiSettings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _emailApiSettings.ApiKey);
        }

        public async Task<bool> SendEmailAsync(string toEmail, string toName, string subject, string html, string text)
        {
            var message = new EmailMessage(
                From: new EmailAddress(_emailApiSettings.FromEmail, _emailApiSettings.FromName),
                To: new List<EmailAddress> { new(toEmail, toName) },
                Subject: subject,
                Html: html,
                Text: text
            );

            var response = await _httpClient.PostAsJsonAsync($"/v1/email", message);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Email successfully sent to {ToName} <{ToEmail}>. StatusCode: {StatusCode}", toName, toEmail, response.StatusCode);
                return true;
            }

            var error = await response.Content.ReadAsStringAsync();
            _logger.LogWarning("Failed to send email to {ToName} <{ToEmail}>. StatusCode: {StatusCode}. Response: {ErrorContent}",
                    toName, toEmail, response.StatusCode, error);

            return false;
        }
    }
}
