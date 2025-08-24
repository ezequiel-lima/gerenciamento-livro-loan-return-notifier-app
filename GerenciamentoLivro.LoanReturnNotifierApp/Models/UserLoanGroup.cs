using Microsoft.Extensions.Logging;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Models
{
    public class UserLoanGroup
    {
        public UserLoanGroup(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            BookTitles = new List<string>();
        }

        public Guid UserId { get; }
        public string UserName { get; }
        public List<string> BookTitles { get; }   

        public void AddBook(string bookTitle)
        {
            if (!string.IsNullOrEmpty(bookTitle))
                BookTitles.Add(bookTitle);
        }
    }
}
