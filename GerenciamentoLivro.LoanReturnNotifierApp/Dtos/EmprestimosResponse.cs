namespace GerenciamentoLivro.LoanReturnNotifierApp.Dtos
{
    public class EmprestimosResponse
    {
        public List<EmprestimoItem> Itens { get; set; } = new();
        public int TotalItens { get; set; }
        public int NumeroPagina { get; set; }
        public int TamanhoPagina { get; set; }
    }
}
