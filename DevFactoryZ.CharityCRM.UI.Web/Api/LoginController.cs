using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels;
using System;
using DevFactoryZ.CharityCRM.Persistence;
using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.UI.Web.Configuration;
using Microsoft.Extensions.Configuration;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IAccountSessionService accountSessionService;
        private readonly ISessionConfig sessionConfig;
        private readonly ICookieConfig cookieConfig;
       
        public LoginController(
            IAccountService accountService
            , IAccountSessionService accountSessionService
            , IConfiguration configuration)
        {
            this.accountService = accountService;
            this.accountSessionService = accountSessionService;
            sessionConfig = configuration.GetSection(nameof(SessionConfig)).Get<SessionConfig>();
            cookieConfig = configuration.GetSection(nameof(CookieConfig)).Get<CookieConfig>();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] LoginViewModel LoginViewModel)
        {
            try
            {
                var account = GetAccount(LoginViewModel?.Login ?? string.Empty, accountService);

                if (!account.Authenticate(LoginViewModel?.Password?.ToCharArray() ?? Array.Empty<char>()))
                {
                    throw new InvalidOperationException($"Пользователь '{LoginViewModel?.Login ?? string.Empty}' не прошел аутентификацию.");
                }

                AddAccountSessionToContext(account);

                HttpContext.Response.StatusCode = 200;

                await HttpContext.Response.WriteAsync($"Пользователь '{LoginViewModel.Login}' аутентифицирован.");
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 400;

                await HttpContext.Response.WriteAsync($"{ex.GetExceptionMessage()}");
            }

            return new StatusCodeResult(HttpContext.Response.StatusCode);
        }

        private void AddAccountSessionToContext(Account account)
        {
            var accountSession = CreateNewAccountSession(
                account
                , HttpContext.GetUserAgent()
                , HttpContext.GetIpAddress()
                , accountSessionService);

            HttpContext.Session.Set(nameof(AccountSession), accountSession.ToJson());

            HttpContext.Response.Cookies.Append(cookieConfig.Name
                , accountSession.Id.ToString()
                , new CookieOptions()
                {
                    HttpOnly = cookieConfig.HttpOnly
                    , IsEssential = cookieConfig.IsEssential
                    , Expires = DateTime.UtcNow.Add(cookieConfig.Expiration ?? new TimeSpan())
                });
        }

        private Account GetAccount(
            string login
            , IAccountService service)
        {
            return service?.GetByLogin(login) ?? 
                throw new ArgumentNullException(nameof(service), $"Сервис '{nameof(IAccountService)}' не инициализирован.");
        }

        private AccountSession CreateNewAccountSession(
            Account account
            , string userAgent
            , string ipAddress
            , IAccountSessionService sessionService)
        {
            var newAccountSession = new AccountSession(
                account
                , userAgent
                , ipAddress
                , DateTime.UtcNow.Add(sessionConfig.AccountSessionIdleTimeout));

            return sessionService?.Create(newAccountSession) 
                ?? throw new ArgumentNullException(nameof(sessionService), $"Сервис '{nameof(IAccountSessionService)}' не инициализирован.");
        }
    }
}
