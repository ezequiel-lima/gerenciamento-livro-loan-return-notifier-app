using GerenciamentoLivro.LoanReturnNotifierApp.Configurations;
using GerenciamentoLivro.LoanReturnNotifierApp.Helpers;
using GerenciamentoLivro.LoanReturnNotifierApp.HttpClients;
using GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Services
{
    public class LoanReminderService : ILoanReminderService
    {
        private readonly ILogger _logger;
        private readonly ILoanHttpClient _httpClient;
        private readonly INotificationService _notificationService;
        private readonly PaginationSettings _paginationSettings;

        public LoanReminderService(
            ILoggerFactory loggerFactory, 
            ILoanHttpClient httpClient, 
            INotificationService notificationService, 
            IOptions<PaginationSettings> paginationSettings)
        {
            _logger = loggerFactory.CreateLogger<LoanReminderService>();
            _httpClient = httpClient;
            _notificationService = notificationService;
            _paginationSettings = paginationSettings.Value;
        }

        public async Task ProcessOverdueLoans()
        {
            int currentPage = 0;
            bool shouldContinue = true;

            var helper = new UserLoanNotificationHelper(_notificationService);

            while (shouldContinue)
            {
                var response = await _httpClient.GetLoansPaginated(currentPage, _paginationSettings.PageSize);

                if (response is null || response.Items.Count == 0)
                {
                    _logger.LogInformation("No loan records found on page {currentPage}", currentPage);
                    break;
                }

                foreach (var loan in response.Items)
                    await helper.ProcessLoanAsync(loan);
                
                currentPage++;
                shouldContinue = response.Items.Count == _paginationSettings.PageSize;
            }

            await helper.SendUserLoanGroupAsync();
        }
    }
}
