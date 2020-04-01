namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class AccountModel
    {
        public AccountModel()
        {
        }

        public AccountModel(Account account)
        {
            Id = account.Id;
            Login = account.Login;
        }

        public int Id { get; set; }

        public string Login { get; set; }
    }
}
