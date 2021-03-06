﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Http;
using DevFactoryZ.CharityCRM.UI.Web.Configuration;
using DevFactoryZ.CharityCRM.Services;

namespace DevFactoryZ.CharityCRM.UI.Web.Middleware
{
    /// <summary>
    /// Аутентификация на основе Cookies с проверкой подлинности HTTP-запроса.
    /// </summary>
    public class CharityAuthentication
    {
        private readonly RequestDelegate next;
        private readonly ICookieConfig cookieConfig;

        #region .ctor

        public CharityAuthentication(
            RequestDelegate next
            , ICookieConfig cookieConfig)
        {
            this.next = next;
            this.cookieConfig = cookieConfig ?? throw new ArgumentNullException(nameof(cookieConfig));
        }

        #endregion

        public async Task InvokeAsync(HttpContext context, IAccountSessionService accountSessionService)
        {
            var accountSessionId = GetAccountSessionId(context);

            if (accountSessionId != Guid.Empty)
            {
                await Authenticate(context, accountSessionService, accountSessionId);
            }

            await next.Invoke(context);
        }
        
        private Guid GetAccountSessionId(HttpContext context)
        {
            return context.Request.Cookies.TryGetValue(cookieConfig.Name, out string value)
                ? (Guid.TryParse(value, out Guid guid)
                    ? guid
                    : throw new HttpRequestException("Неверный формат идентификатора пользовательской сессии."))
                : Guid.Empty;
        }

        private async Task Authenticate(HttpContext context, IAccountSessionService accountSessionService, Guid sessionId)
        {
            AccountSession accountSession;

            if (context.Session.TryGetValue(nameof(AccountSession), out byte[] value))
            {
                accountSession = await value.FromJsonAsync<AccountSession>();
            }
            else
            {
                accountSession = accountSessionService.GetById(sessionId);
                context.Session.Set(nameof(AccountSession), accountSession.ToJson());
            }

            if (!accountSession.IsAlive()
                || !accountSession.IsReliable(context.GetUserAgent(), context.GetIpAddress()))
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

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
        }
    }
}
