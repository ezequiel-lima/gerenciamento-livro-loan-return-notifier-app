using GerenciamentoLivro.LoanReturnNotifierApp.Configurations;
using GerenciamentoLivro.LoanReturnNotifierApp.HttpClients;
using GerenciamentoLivro.LoanReturnNotifierApp.Models;
using GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Services
{
    public class LoanReminderService : ILoanReminderService
    {
        private readonly ILogger _logger;
        private readonly ILoanHttpClient _httpClient;
        private readonly IEmailNotificationService _notificationService;
        private readonly PaginationSettings _paginationSettings;

        public LoanReminderService(
            ILoggerFactory loggerFactory,
            ILoanHttpClient httpClient,
            IEmailNotificationService notificationService,
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
            var usersNotified = new HashSet<Guid>();

            while (shouldContinue)
            {
                var response = await _httpClient.GetLoansPaginated(currentPage, _paginationSettings.PageSize);

                if (response is null || response.Items.Count == 0)
                {
                    _logger.LogInformation("No loan records found on page {currentPage}", currentPage);
                    break;
                }

                var userModels = response.Items
                    .GroupBy(x => x.UserId)
                    .Select(group =>
                    {
                        var first = group.First();

                        if (!usersNotified.Add(group.Key))
                            return null;

                        return new UserModel(
                            userId: group.Key,
                            userName: first.UserName ?? "Unknown Name",
                            userEmail: first.UserEmail ?? "Unknown Email",
                            bookTitles: group.Select(x => x.BookTitle ?? "Unknown Book Title").ToList()
                        );
                    })
                    .Where(model => model is not null);

                foreach (var user in userModels)
                {
                    await _notificationService.NotifyUserLoanAsync(user!);
                }

                currentPage++;
                shouldContinue = response.Items.Count == _paginationSettings.PageSize;
            }           
        }
    }
}
