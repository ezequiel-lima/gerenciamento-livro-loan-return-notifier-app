using GerenciamentoLivro.LoanReturnNotifierApp.Dtos;
using GerenciamentoLivro.LoanReturnNotifierApp.Models;
using GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Helpers
{
    public class UserLoanNotificationHelper
    {
        private readonly INotificationService _notificationService;
        private UserLoanGroup? _currentGroup;

        public UserLoanNotificationHelper(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task ProcessLoanAsync(LoanItemDto loan)
        {
            if (_currentGroup == null || _currentGroup.UserId != loan.UserId)
            {
                await SendUserLoanGroupAsync();
                _currentGroup = new UserLoanGroup(loan.UserId, loan.UserName ?? "Unknown");
            }

            _currentGroup.AddBook(loan.BookTitle ?? "Title not provided");
        }

        public async Task SendUserLoanGroupAsync()
        {
            if (_currentGroup is not null)
                await _notificationService.NotifyUserLoanAsync(_currentGroup);          
        }
    }
}
