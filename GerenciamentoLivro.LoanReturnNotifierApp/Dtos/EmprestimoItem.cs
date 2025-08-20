namespace GerenciamentoLivro.LoanReturnNotifierApp.Dtos
{
    public class EmprestimoItem
    {
        public Guid IdUsuario { get; set; }
        public string? NomeUsuario { get; set; }
        public Guid IdLivro { get; set; }
        public string? TituloLivro { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public DateTime? DataDevolucaoEfetiva { get; set; }
    }
}
