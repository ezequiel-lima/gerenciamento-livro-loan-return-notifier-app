namespace GerenciamentoLivro.LoanReturnNotifierApp.Services
{
    public interface ILoanReminderService
    {
        Task ProcessOverdueLoans();
    }
}