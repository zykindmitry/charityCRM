namespace DevFactoryZ.CharityCRM.UI.Web.Api.Models
{
    /// <summary>
    /// ViewModel для LoginController.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Идентификатор (логин) пользователя, переданный в HTTP-запросе на аутентификацию.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя, переданный в HTTP-запросе на аутентификацию.
        /// </summary>
        public string Password { get; set; }
    }
}
