using Microsoft.Extensions.Logging;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Models
{
    public class UserModel
    {
        public UserModel(Guid userId, string userName, List<string> bookTitles)
        {
            UserId = userId;
            UserName = userName;
            BookTitles = bookTitles;
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<string> BookTitles { get; set; }   
    }
}
