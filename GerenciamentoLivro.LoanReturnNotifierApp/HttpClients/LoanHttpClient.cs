using GerenciamentoLivro.LoanReturnNotifierApp.Configurations;
using GerenciamentoLivro.LoanReturnNotifierApp.Dtos;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace GerenciamentoLivro.LoanReturnNotifierApp.HttpClients
{
    public class LoanHttpClient : ILoanHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly LoanApiSettings _loanApiSettings;

        public LoanHttpClient(IHttpClientFactory httpClientFactory, IOptions<LoanApiSettings> loanApiSettings)
        {
            _httpClient = httpClientFactory.CreateClient();
            _loanApiSettings = loanApiSettings.Value;
        }

        public async Task<LoansResponseDto?> GetLoansPaginated(int pageNumber, int pageSize)
        {
            var url = $"{_loanApiSettings.BaseUrl}{_loanApiSettings.OverdueEndpoint}?numeroPagina={pageNumber}&tamanhoPagina={pageSize}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<LoansResponseDto>();
        }
    }
}
