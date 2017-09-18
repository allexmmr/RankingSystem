using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RankingSystem.Data;
using RankingSystem.Data.Models;
using RankingSystem.Web.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RankingSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly RankingSystemContext _context;

        public AccountController(RankingSystemContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Member member)
        {
            try
            {
                Member m = await _context.Members.SingleOrDefaultAsync(q => q.Email == member.Email);

                if (m != null)
                {
                    HttpContext.Session.SetString("MemberId", m.Id.ToString());
                    HttpContext.Session.SetString("MemberEmail", m.Email);
                    HttpContext.Session.SetString("MemberIsAdmin", m.IsAdmin.ToString());

                    return RedirectToAction("Index", "Home");
                }

                return View(new LoginViewModel { Error = "Email not found or invalid." });
            }
            catch (Exception ex)
            {
                return View(new LoginViewModel { Error = ex.Message });
            }
        }

        public IActionResult Logout()
        {
            // It will clear the session at the end of request
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }
    }
}