using System.Text.Json.Serialization;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Dtos
{
    public class LoanItem
    {
        [JsonPropertyName("idUsuario")]
        public Guid UserId { get; set; }

        [JsonPropertyName("nomeUsuario")]
        public string? UserName { get; set; }

        [JsonPropertyName("idLivro")]
        public Guid BookId { get; set; }

        [JsonPropertyName("tituloLivro")]
        public string? BookTitle { get; set; }

        [JsonPropertyName("dataEmprestimo")]
        public DateTime LoanDate { get; set; }

        [JsonPropertyName("dataDevolucaoPrevista")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("dataDevolucaoEfetiva")]
        public DateTime? ReturnDate { get; set; }
    }
}
