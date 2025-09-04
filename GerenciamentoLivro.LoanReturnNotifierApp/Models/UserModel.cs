namespace GerenciamentoLivro.LoanReturnNotifierApp.Models
{
    public class UserModel
    {
        public UserModel(Guid userId, string userName, string userEmail, List<string> bookTitles)
        {
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
            BookTitles = bookTitles;
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<string> BookTitles { get; set; }   
    }
}
