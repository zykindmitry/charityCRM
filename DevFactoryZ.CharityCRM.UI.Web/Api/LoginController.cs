using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post()
        {
            try
            {
                var accountSession = HttpContext.Session.TryGetValue(nameof(AccountSession), out byte[] value)
                    ? value.FromJson<AccountSession>()
                    : throw new ValidationException("Время жизни сессии пользователя истекло.");
            }
            catch (Exception ex)
            {
                await HttpContext.Response.WriteAsync($"{ex.GetExceptionMessage()}");
                HttpContext.Response.StatusCode = 400;
            }

            return new StatusCodeResult(HttpContext.Response.StatusCode);
        }
    }
}
