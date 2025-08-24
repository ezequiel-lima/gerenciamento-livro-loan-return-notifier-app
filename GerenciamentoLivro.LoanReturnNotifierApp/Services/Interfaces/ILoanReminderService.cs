namespace GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces
{
    public interface ILoanReminderService
    {
        Task ProcessOverdueLoans();
    }
}