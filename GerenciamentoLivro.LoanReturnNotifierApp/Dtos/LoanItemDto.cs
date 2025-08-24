using System.Text.Json.Serialization;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Dtos
{
    public class LoanItemDto
    {
        [JsonPropertyName("idUsuario")]
        public Guid UserId { get; set; }

        [JsonPropertyName("nomeUsuario")]
        public string? UserName { get; set; }

        [JsonPropertyName("tituloLivro")]
        public string? BookTitle { get; set; }

        [JsonPropertyName("dataDevolucaoPrevista")]
        public DateTime DueDate { get; set; }
    }
}
