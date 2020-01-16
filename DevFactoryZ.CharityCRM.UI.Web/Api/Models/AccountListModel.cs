namespace DevFactoryZ.CharityCRM.UI.Web.Api.Models
{
    public class AccountListModel : AccountModel
    {
        public AccountListModel() : base()
        {
        }

        public AccountListModel(Account account)
        {
            Id = account.Id;
            Login = account.Login;
            //Password = account.Password;
        }

        public int Id { get; set; }
    }
}
