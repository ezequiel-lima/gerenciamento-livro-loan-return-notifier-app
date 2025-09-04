using GerenciamentoLivro.LoanReturnNotifierApp.Models;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces
{
    public interface IEmailNotificationService
    {
        Task NotifyUserLoanAsync(UserModel userModel);
    }
}
