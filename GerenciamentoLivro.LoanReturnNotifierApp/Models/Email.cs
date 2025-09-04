namespace GerenciamentoLivro.LoanReturnNotifierApp.Models
{
    public record EmailAddress(string Email, string Name);

    public record EmailMessage(
        EmailAddress From,
        IReadOnlyList<EmailAddress> To,
        string Subject,
        string Html,
        string Text
    );
}
