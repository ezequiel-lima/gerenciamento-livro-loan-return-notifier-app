using GerenciamentoLivro.LoanReturnNotifierApp.Dtos;
using System.Net.Http.Json;

namespace GerenciamentoLivro.LoanReturnNotifierApp.HttpClients
{
    public class LoanHttpClient : ILoanHttpClient
    {
        private readonly HttpClient _httpClient;

        public LoanHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<LoansResponse?> GetLoansPaginated(int pageNumber, int pageSize)
        {
            var url = $"https://localhost:7140/api/Emprestimos?numeroPagina={pageNumber}&tamanhoPagina={pageSize}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<LoansResponse>();
        }
    }
}
