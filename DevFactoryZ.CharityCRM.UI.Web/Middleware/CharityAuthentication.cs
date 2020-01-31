using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using DevFactoryZ.CharityCRM.UI.Web.Configuration;
using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.Persistence;
using DevFactoryZ.CharityCRM.UI.Web.Api.Models;

namespace DevFactoryZ.CharityCRM.UI.Web.Middleware
{
    /// <summary>
    /// Аутентификация на основе Cookies с проверкой подлинности HTTP-запроса.
    /// </summary>
    public class CharityAuthentication
    {
        private readonly RequestDelegate next;
        private readonly IRepositoryCreatorFactory repositoryCreatorFactory;
        private readonly IAccountService accountService;
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
            , IAccountService accountService
            , IAccountSessionService accountSessionService
            , ISessionConfig sessionConfig
            , ICookieConfig cookieConfig)
            : this(next, sessionConfig, cookieConfig)
        {
            this.accountService = accountService;
            this.accountSessionService = accountSessionService;
        }

        #endregion

        public async Task InvokeAsync(HttpContext context)
        {
            #region Проверка параметров метода - содержимого HttpContext.

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), $"Некорректный запрос: {nameof(context)} is null.");
            }
            else if (context.Request == null)
            {
                throw new ArgumentException($"Некорректный запрос: {nameof(context.Request)} is null.", nameof(context.Request));
            }
            else
            {
                if (context.Request.Cookies == null)
                {
                    throw new ArgumentException($"Некорректный запрос: {nameof(context.Request.Cookies)} is null.", nameof(context.Request.Cookies));
                }

                if (context.Request.Body == null)
                {
                    throw new ArgumentException($"Некорректный запрос: {nameof(context.Request.Body)} is null.", nameof(context.Request.Body));
                }
            }

            #endregion

            #region Определение AccountSessionId из HTTP-запроса

            var accountSessionId = context.Request.Cookies.TryGetValue(cookieConfig.Name, out string value)
                ? (Guid.TryParse(value, out Guid guid)
                    ? guid
                    : throw new ValidationException("Неверный формат идентификатора пользовательской сессии."))
                : Guid.Empty;

            #endregion

            #region Аутентификация

            if (context.Request.ContentLength != null)
            {
                await Authenticate(context, accountSessionId);
            }

            #endregion

            await next.Invoke(context);
        }
        
        private async Task Authenticate(HttpContext context, Guid sessionId)
        {
            var model = await context.Request.Body.FromJsonAsync<LoginViewModel>();

            var accountSession = sessionId != Guid.Empty
                ? GetAccountSessionBy(sessionId, repositoryCreatorFactory)
                : CreateNewAccountSession(repositoryCreatorFactory, model, context.GetUserAgent(), context.GetIpAddress());

            if (accountSession.IsNoFake(context)
                && accountSession.IsNoExpired()
                && accountSession.IsAuthenticated(model?.Password))
            {
                accountSession.ExtendSession(sessionConfig.IdleTimeout);

                accountSession.UpdateRepository(repositoryCreatorFactory);

                context.Session.Set(nameof(AccountSession), accountSession.ToJson());

                context.Response.Cookies.Append(cookieConfig.Name
                    , accountSession.Id.ToString()
                    , new CookieOptions()
                    {
                        HttpOnly = cookieConfig.HttpOnly
                        ,
                        IsEssential = cookieConfig.IsEssential
                        ,
                        Expires = DateTime.UtcNow.Add(cookieConfig.Expiration ?? new TimeSpan())
                    });

                context.Response.StatusCode = 200;
            }
            else
            {
                context.Session.Remove(nameof(AccountSession));

                context.Response.Cookies.Append(cookieConfig.Name
                    , accountSession.Id.ToString()
                    , new CookieOptions()
                    {
                        Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10).Negate())
                        ,
                        HttpOnly = cookieConfig.HttpOnly
                        ,
                        IsEssential = cookieConfig.IsEssential
                    });

                context.Response.StatusCode = 400;
            }
        }

        private AccountSession CreateNewAccountSession(IRepositoryCreatorFactory repositoryCreator, LoginViewModel model, string userAgent, string ipAddress)
        {
            var accountRepository = repositoryCreator?
                .GetRepositoryCreator<IAccountRepository>()?
                .Create() ?? 
                throw new ArgumentException(
                    $"Фабрика создателей репозиториев '{nameof(IRepositoryCreatorFactory)}' не инициализирована или не содержит '{nameof(IAccountRepository)}'."
                    , nameof(repositoryCreator));

            var accountSessionRepository = repositoryCreator?
                .GetRepositoryCreator<IAccountSessionRepository>()?
                .Create() ??
                throw new ArgumentException(
                    $"Фабрика создателей репозиториев '{nameof(IRepositoryCreatorFactory)}' не инициализирована или не содержит '{nameof(IAccountSessionRepository)}'."
                    , nameof(repositoryCreator));

            var account = accountRepository.GetByLogin(model?.Login);

            var newAccountSession = new AccountSession(account
                , userAgent
                , ipAddress
                , DateTime.UtcNow.Add(sessionConfig.IdleTimeout));

            accountSessionRepository.Create(newAccountSession);
            accountSessionRepository.Save();

            return newAccountSession;
        }

        private AccountSession CreateNewAccountSession(IAccountService serviceAccount, IAccountSessionService sessionService, LoginViewModel model, string userAgent, string ipAddress)
        {
            var account = serviceAccount?.GetByLogin(model?.Login) ??
                throw new ArgumentNullException(
                    nameof(serviceAccount)
                    , $"Сервис '{nameof(IAccountService)}' не инициализирован.");

            var newAccountSession = new AccountSession(account
                , userAgent
                , ipAddress
                , DateTime.UtcNow.Add(sessionConfig.IdleTimeout));

            return sessionService?.Create(newAccountSession) ??
                throw new ArgumentNullException(
                    nameof(sessionService)
                    , $"Сервис '{nameof(IAccountSessionService)}' не инициализирован.");
        }

        private AccountSession GetAccountSessionBy(Guid id, IRepositoryCreatorFactory repositoryCreator)
        {
            return repositoryCreator?
                .GetRepositoryCreator<IAccountSessionRepository>()?
                .Create()?
                .GetById(id) ??
                throw new ArgumentException(
                    $"Фабрика создателей репозиториев '{nameof(IRepositoryCreatorFactory)}' не инициализирована или не содержит '{nameof(IAccountSessionRepository)}'."
                    , nameof(repositoryCreator));
        }

        private AccountSession GetAccountSessionBy(Guid id, IAccountSessionService sessionService)
        {
            return sessionService?.GetById(id) ??
                throw new ArgumentNullException(
                    nameof(sessionService)
                    , $"Сервис '{nameof(IAccountSessionService)}' не инициализирован.");
        }
    }
}
