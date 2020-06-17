using Microsoft.AspNetCore.Mvc;

namespace DevFactoryZ.CharityCRM.UI.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {           
            return View();
        }
    }
}
