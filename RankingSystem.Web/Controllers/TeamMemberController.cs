using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RankingSystem.Data;
using RankingSystem.Data.Models;
using RankingSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace RankingSystem.Web.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly RankingSystemContext _context;

        public TeamMemberController(RankingSystemContext context)
        {
            _context = context;
        }

        // GET: TeamMember
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("MemberId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(await _context.Teams.OrderBy(q => q.Name).ToListAsync());
        }

        // GET: TeamMember/Details/5
        public IActionResult Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<TeamMember> teamMembers = (from tm in _context.TeamMembers.AsEnumerable()
                                            join t in _context.Teams on tm.TeamId equals t.Id
                                            join m in _context.Members on tm.MemberId equals m.Id
                                            where t.Id == id
                                            select new TeamMember()
                                            {
                                                Id = tm.Id,
                                                TeamId = tm.TeamId,
                                                Team = new Team() { Id = t.Id, Name = t.Name },
                                                MemberId = tm.MemberId,
                                                Member = new Member() { Id = m.Id, FullName = m.FullName, Email = m.Email, Code = m.Code, Status = m.Status, IsAdmin = m.IsAdmin }
                                            }).OrderBy(q => q.Member.FullName).ToList();

            if (teamMembers == null)
            {
                return NotFound();
            }

            return View(teamMembers);
        }

        // GET: TeamMember/Assign
        public IActionResult Assign()
        {
            TeamMemberViewModel model = new TeamMemberViewModel();

            model.Members = (from m in _context.Members.AsEnumerable()
                             orderby m.FullName
                             select new Member()
                             {
                                 Id = m.Id,
                                 FullName = string.Format("{0} | {1}", m.FullName, m.Email)
                             }).ToList();

            return PartialView(model);
        }

        // POST: TeamMember/Assign
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign([Bind("MemberId")] TeamMember teamMember)
        {
            string sTeamId = HttpContext.Session.GetString("TeamId");

            if (sTeamId == null || teamMember.MemberId <= 0)
            {
                return PartialView("Error", "Shared");
            }

            short teamId = Convert.ToInt16(sTeamId);

            bool assigned = await _context.TeamMembers.AnyAsync(q => q.TeamId == teamId && q.MemberId == teamMember.MemberId);

            if (assigned)
            {
                return PartialView("Error", "Shared");
            }

            if (ModelState.IsValid)
            {
                teamMember.TeamId = teamId;
                _context.TeamMembers.Add(teamMember);
                await _context.SaveChangesAsync();

                return RedirectToAction("Successfully");
            }

            return PartialView(teamMember);
        }

        // GET: TeamMember/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamMember teamMember = (from tm in _context.TeamMembers.AsEnumerable()
                                     join t in _context.Teams on tm.TeamId equals t.Id
                                     join m in _context.Members on tm.MemberId equals m.Id
                                     where tm.Id == id
                                     select new TeamMember()
                                     {
                                         Id = tm.Id,
                                         TeamId = tm.TeamId,
                                         Team = new Team() { Id = t.Id, Name = t.Name },
                                         MemberId = tm.MemberId,
                                         Member = new Member() { Id = m.Id, FullName = m.FullName, Email = m.Email, Code = m.Code, Status = m.Status, IsAdmin = m.IsAdmin }
                                     }).SingleOrDefault();

            if (teamMember == null)
            {
                return NotFound();
            }

            return PartialView(teamMember);
        }

        // POST: TeamMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TeamMember teamMember = await _context.TeamMembers.SingleOrDefaultAsync(q => q.Id == id);
            _context.TeamMembers.Remove(teamMember);
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