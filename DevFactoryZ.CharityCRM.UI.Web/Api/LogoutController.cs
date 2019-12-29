using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")] 
    public class LogoutController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
