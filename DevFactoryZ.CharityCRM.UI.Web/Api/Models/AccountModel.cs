using DevFactoryZ.CharityCRM.Services;

namespace DevFactoryZ.CharityCRM.UI.Web.Api.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
        }

        public string Login { get; set; }

        public char[] PasswordClearText { get; set; }

        public IPasswordConfig PasswordConfig { get; set; }

        public Account ToDomain()
        {
            return new Account(Login, PasswordClearText, PasswordConfig);
        }
    }
}
