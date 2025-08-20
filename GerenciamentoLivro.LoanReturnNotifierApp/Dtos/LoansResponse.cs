using System.Text.Json.Serialization;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Dtos
{
    public class LoansResponse
    {
        [JsonPropertyName("itens")]
        public List<LoanItem> Items { get; set; } = new();

        [JsonPropertyName("totalItens")]
        public int TotalItems { get; set; }

        [JsonPropertyName("numeroPagina")]
        public int PageNumber { get; set; }

        [JsonPropertyName("tamanhoPagina")]
        public int PageSize { get; set; }
    }
}
