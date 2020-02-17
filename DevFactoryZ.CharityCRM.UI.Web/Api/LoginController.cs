using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels;
using System;
using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.UI.Web.Configuration;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly ICookieConfig cookieConfig;
       
        public LoginController(
            IAccountService accountService
            , IConfiguration configuration)
        {
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

            cookieConfig = configuration?.GetSection(nameof(CookieConfig))?.Get<CookieConfig>() 
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel == null || string.IsNullOrWhiteSpace(loginViewModel.Login))
            {
                return BadRequest("Требуется логин.");
            }

            try
            {
                AccountSession accountSession;

                if (HttpContext.Session.TryGetValue(nameof(AccountSession), out byte[] value))
                {
                    accountSession = await value.FromJsonAsync<AccountSession>();
                }
                else
                {
                    accountSession = accountService.Login(loginViewModel.Login, loginViewModel.Password, HttpContext.GetUserAgent(), HttpContext.GetIpAddress());

                    HttpContext.Session.Set(nameof(AccountSession), accountSession.ToJson());
                }

                HttpContext.Response.Cookies.Append(cookieConfig.Name
                    , accountSession.Id.ToString()
                    , new CookieOptions()
                    {
                        HttpOnly = cookieConfig.HttpOnly
                        , IsEssential = cookieConfig.IsEssential
                        , Expires = DateTime.UtcNow.Add(cookieConfig.Expiration ?? new TimeSpan())
                    });

                return Ok("Пользователь аутентифицирован.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
