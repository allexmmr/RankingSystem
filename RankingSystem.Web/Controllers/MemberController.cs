using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RankingSystem.Data;
using RankingSystem.Data.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using static RankingSystem.Common.Domain.Enums;

namespace RankingSystem.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly RankingSystemContext _context;

        public MemberController(RankingSystemContext context)
        {
            _context = context;
        }

        // GET: Member
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("MemberId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(await _context.Members.OrderBy(q => q.FullName).ToListAsync());
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Members.SingleOrDefaultAsync(q => q.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            return PartialView(member);
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Email")] Member member)
        {
            string isAdmin = HttpContext.Session.GetString("MemberIsAdmin").ToLower();
            if (isAdmin != "true")
            {
                TempData["ErrorMessage"] = "We're sorry, only admin can perform this operation.";
                return PartialView("Error", "Shared");
            }

            if (ModelState.IsValid)
            {
                string code = Guid.NewGuid().ToString();
                string url = "http://localhost:60426/Member/Activate?code=" + code;
                member.Code = code;
                member.Status = StatusEnum.Pending;
                TempData["Message"] = string.Format("An email will be sent to {0} for the user to activate access.<br />Administrators can activate this user simply by clicking on the link below:<br /><a href='{1}'>{1}</a>", member.Email, url);
                _context.Members.Add(member);
                await _context.SaveChangesAsync();

                return RedirectToAction("Successfully");
            }

            return PartialView(member);
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Members.SingleOrDefaultAsync(q => q.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            return PartialView(member);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Status,IsAdmin")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Members.Attach(member);
                _context.Entry(member).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction("Successfully");
            }

            return PartialView(member);
        }

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Members.SingleOrDefaultAsync(q => q.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            return PartialView(member);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string isAdmin = HttpContext.Session.GetString("MemberIsAdmin").ToLower();
            if (isAdmin != "true")
            {
                TempData["ErrorMessage"] = "We're sorry, only admin can perform this operation.";
                return PartialView("Error", "Shared");
            }

            Member member = await _context.Members.SingleOrDefaultAsync(q => q.Id == id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return RedirectToAction("Successfully");
        }

        // GET: Member/Activate/{Code}
        public async Task<IActionResult> Activate(string code)
        {
            if (code == null)
            {
                return NotFound();
            }

            Member member = await _context.Members.SingleOrDefaultAsync(q => q.Code == code);

            if (member == null)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    member.Status = StatusEnum.Activated;
                    _context.Members.Attach(member);
                    _context.Entry(member).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Successfully");
                }
            }

            return PartialView(member);
        }

        // GET: Successfully
        public ActionResult Successfully()
        {
            return PartialView("Successfully", "Shared");
        }
    }
}