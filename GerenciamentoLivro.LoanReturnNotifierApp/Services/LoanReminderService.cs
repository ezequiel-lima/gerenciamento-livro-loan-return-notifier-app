using GerenciamentoLivro.LoanReturnNotifierApp.Dtos;
using GerenciamentoLivro.LoanReturnNotifierApp.HttpClients;
using Microsoft.Extensions.Logging;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Services
{
    public class LoanReminderService : ILoanReminderService
    {
        private readonly ILogger _logger;
        private readonly ILoanHttpClient _httpClient;
        private const int PageSize = 100;

        public LoanReminderService(ILoggerFactory loggerFactory, ILoanHttpClient httpClient)
        {
            _logger = loggerFactory.CreateLogger<LoanReminderService>();
            _httpClient = httpClient;
        }

        public async Task ProcessOverdueLoans()
        {
            int currentPage = 0;
            bool shouldContinue = true;

            while (shouldContinue)
            {
                var loans = await _httpClient.GetLoansPaginated(currentPage, PageSize);

                if (loans is null || loans.Items.Count == 0)
                {
                    _logger.LogInformation("No loan records found on page {currentPage}. Stopping iteration.", currentPage);
                    break;
                }

                var overdueLoans = loans.Items
                    .Where(x => x.ReturnDate == null && x.DueDate.Date < DateTime.Now.Date)
                    .ToList();

                foreach (var overdueLoan in overdueLoans)
                {
                    _logger.LogInformation("User {user} has an overdue loan for book {book} since {date}.",
                        overdueLoan.UserName, overdueLoan.BookTitle, overdueLoan.DueDate.ToShortDateString());
                }

                currentPage++;
                shouldContinue = loans.Items.Count == PageSize;
            }
        }
    }
}
