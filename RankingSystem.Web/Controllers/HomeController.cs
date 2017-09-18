using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RankingSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("MemberId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}