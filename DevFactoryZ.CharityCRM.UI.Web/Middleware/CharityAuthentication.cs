using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using DevFactoryZ.CharityCRM.UI.Web.Configuration;
using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.Persistence;
using DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels;

namespace DevFactoryZ.CharityCRM.UI.Web.Middleware
{
    /// <summary>
    /// Аутентификация на основе Cookies с проверкой подлинности HTTP-запроса.
    /// </summary>
    public class CharityAuthentication
    {
        private readonly RequestDelegate next;
        private readonly IRepositoryCreatorFactory repositoryCreatorFactory;
        private readonly IAccountSessionService accountSessionService;
        private readonly ISessionConfig sessionConfig;
        private readonly ICookieConfig cookieConfig;

        #region .ctor

        protected CharityAuthentication(RequestDelegate next
            , ISessionConfig sessionConfig
            , ICookieConfig cookieConfig)
        {
            this.next = next;
            this.sessionConfig = sessionConfig;
            this.cookieConfig = cookieConfig;
        }

        public CharityAuthentication(RequestDelegate next
            , IRepositoryCreatorFactory repositoryCreatorFactory
            , ISessionConfig sessionConfig
            , ICookieConfig cookieConfig)
            : this(next, sessionConfig, cookieConfig)
        {
            this.repositoryCreatorFactory = repositoryCreatorFactory;
        }

        public CharityAuthentication(RequestDelegate next
            , IAccountSessionService accountSessionService
            , ISessionConfig sessionConfig
            , ICookieConfig cookieConfig)
            : this(next, sessionConfig, cookieConfig)
        {
            this.accountSessionService = accountSessionService;
        }

        #endregion

        public async Task InvokeAsync(HttpContext context)
        {
            #region Определение AccountSessionId из HTTP-запроса

            var accountSessionId = context.Request.Cookies.TryGetValue(cookieConfig.Name, out string value)
                ? (Guid.TryParse(value, out Guid guid)
                    ? guid
                    : throw new ValidationException("Неверный формат идентификатора пользовательской сессии.")) // Позже заменится на установку нужного StatusCode
                : Guid.Empty;

            #endregion

            #region Аутентификация

            if (accountSessionId != Guid.Empty)
            {
                await Authenticate(context, accountSessionId);
            }

            #endregion

            await next.Invoke(context);
        }
        
        private async Task Authenticate(HttpContext context, Guid sessionId)
        {
            var isAccountSessionFromContext = context.Session.TryGetValue(nameof(AccountSession), out byte[] value);

            AccountSession accountSession;

            if (isAccountSessionFromContext)
            {
                accountSession = await value.FromJsonAsync<AccountSession>();
            }
            else
            {
                accountSession = GetAccountSessionBy(sessionId, repositoryCreatorFactory);
                context.Session.Set(nameof(AccountSession), accountSession.ToJson());
            }

            if (!accountSession.IsAlive()
                || !accountSession.IsReliable(context))
            {
                context.Session.Remove(nameof(AccountSession));

                context.Response.Cookies.Append(cookieConfig.Name
                    , accountSession.Id.ToString()
                    , new CookieOptions()
                    {
                        Expires = DateTime.UtcNow.Add(TimeSpan.FromDays(3650).Negate())
                        , HttpOnly = cookieConfig.HttpOnly
                        , IsEssential = cookieConfig.IsEssential
                    });

                context.Response.StatusCode = 400;
            }
        }

        private AccountSession GetAccountSessionBy(
            Guid id
            , IRepositoryCreatorFactory repositoryCreator)
        {
            return repositoryCreator?
                .GetRepositoryCreator<IAccountSessionRepository>()?
                .Create()?
                .GetById(id) ??
                throw new ArgumentException(
                    $"Фабрика создателей репозиториев '{nameof(IRepositoryCreatorFactory)}' не инициализирована или не содержит '{nameof(IAccountSessionRepository)}'."
                    , nameof(repositoryCreator));
        }

        private AccountSession GetAccountSessionBy(
            Guid id
            , IAccountSessionService sessionService)
        {
            return sessionService?.GetById(id) ??
                throw new ArgumentNullException(
                    nameof(sessionService)
                    , $"Сервис '{nameof(IAccountSessionService)}' не инициализирован.");
        }
    }
}
