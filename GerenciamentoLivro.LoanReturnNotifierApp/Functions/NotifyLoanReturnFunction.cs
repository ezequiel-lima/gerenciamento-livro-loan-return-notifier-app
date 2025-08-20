using GerenciamentoLivro.LoanReturnNotifierApp.Dtos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Functions;

public class NotifyLoanReturnFunction
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    private const int TamanhoPagina = 100;

    public NotifyLoanReturnFunction(ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
    {
        _logger = loggerFactory.CreateLogger<NotifyLoanReturnFunction>();
        _httpClient = httpClientFactory.CreateClient();
    }

    [Function("NotifyLoanReturnFunction")]
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("Iniciando verificação de empréstimos em {time}.", DateTime.Now);

        int paginaAtual = 0;
        bool continuarBuscando = true;

        try
        {
            while (continuarBuscando)
            {
                var url = $"https://localhost:7140/api/Emprestimos?numeroPagina={paginaAtual}&tamanhoPagina={TamanhoPagina}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Erro ao buscar empréstimos. Página: {pagina}. Status: {status}",
                        paginaAtual, response.StatusCode);
                    return;
                }

                var emprestimos = await response.Content.ReadFromJsonAsync<EmprestimosResponse>();

                if (emprestimos is null || emprestimos.Itens.Count == 0)
                {
                    _logger.LogInformation("Nenhum empréstimo encontrado na página {pagina}. Fim da leitura.", paginaAtual);
                    break;
                }

                var emprestimosAtrasados = emprestimos.Itens
                    .Where(x => x.DataDevolucaoEfetiva == null && x.DataDevolucaoPrevista.Date < DateTime.Now.Date)
                    .ToList();

                foreach (var emprestimosAtrasado in emprestimosAtrasados)
                {
                    _logger.LogInformation("Usuário {usuario} com livro {livro} atrasado desde {data}",
                        emprestimosAtrasado.NomeUsuario, emprestimosAtrasado.TituloLivro, emprestimosAtrasado.DataDevolucaoPrevista.ToShortDateString());
                }

                paginaAtual++;
                continuarBuscando = emprestimos.Itens.Count == TamanhoPagina;
            }

            _logger.LogInformation("Verificação concluída.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception while sending loan return notification.");
        }
    }
}