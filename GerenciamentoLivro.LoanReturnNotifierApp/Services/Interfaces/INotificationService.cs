using GerenciamentoLivro.LoanReturnNotifierApp.Models;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces
{
    public interface INotificationService
    {
        Task NotifyUserLoanAsync(UserLoanGroup userLoanGroup);
    }
}
