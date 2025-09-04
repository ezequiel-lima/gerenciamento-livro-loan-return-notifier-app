namespace GerenciamentoLivro.LoanReturnNotifierApp.HttpClients
{
    public interface IEmailHttpClient
    {
        Task<bool> SendEmailAsync(string toEmail, string toName, string subject, string html, string text);
    }
}