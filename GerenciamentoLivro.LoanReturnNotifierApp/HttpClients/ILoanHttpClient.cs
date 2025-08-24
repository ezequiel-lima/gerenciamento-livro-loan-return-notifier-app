using GerenciamentoLivro.LoanReturnNotifierApp.Dtos;

namespace GerenciamentoLivro.LoanReturnNotifierApp.HttpClients
{
    public interface ILoanHttpClient
    {
        Task<LoansResponseDto?> GetLoansPaginated(int pageNumber, int pageSize);
    }
}