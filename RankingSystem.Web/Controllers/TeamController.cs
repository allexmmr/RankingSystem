using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RankingSystem.Data;
using RankingSystem.Data.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace RankingSystem.Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly RankingSystemContext _context;

        public TeamController(RankingSystemContext context)
        {
            _context = context;
        }

        // GET: Team
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("MemberId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(await _context.Teams.OrderBy(q => q.Name).ToListAsync());
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = await _context.Teams.SingleOrDefaultAsync(q => q.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return PartialView(team);
        }

        // GET: Team/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Team team)
        {
            string isAdmin = HttpContext.Session.GetString("MemberIsAdmin").ToLower();
            if (isAdmin != "true")
            {
                TempData["ErrorMessage"] = "We're sorry, only admin can perform this operation.";
                return PartialView("Error", "Shared");
            }

            if (ModelState.IsValid)
            {
                _context.Teams.Add(team);
                await _context.SaveChangesAsync();

                return RedirectToAction("Successfully");
            }

            return PartialView(team);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = await _context.Teams.SingleOrDefaultAsync(q => q.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return PartialView(team);
        }

        // POST: Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Teams.Attach(team);
                _context.Entry(team).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction("Successfully");
            }

            return PartialView(team);
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = await _context.Teams.SingleOrDefaultAsync(q => q.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return PartialView(team);
        }

        // POST: Team/Delete/5
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

            Team team = await _context.Teams.SingleOrDefaultAsync(q => q.Id == id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return RedirectToAction("Successfully");
        }

        // GET: Successfully
        public ActionResult Successfully()
        {
            return PartialView("Successfully", "Shared");
        }
    }
}