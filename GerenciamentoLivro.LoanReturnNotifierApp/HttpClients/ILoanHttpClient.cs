using GerenciamentoLivro.LoanReturnNotifierApp.Dtos;

namespace GerenciamentoLivro.LoanReturnNotifierApp.HttpClients
{
    public interface ILoanHttpClient
    {
        Task<LoansResponse?> GetLoansPaginated(int pageNumber, int pageSize);
    }
}