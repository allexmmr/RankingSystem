using RankingSystem.Data.Models;
using System;
using System.Linq;
using static RankingSystem.Common.Domain.Enums;

namespace RankingSystem.Data.Migrations
{
    public static class DbInitialiser
    {
        public static void Initialise(RankingSystemContext context)
        {
            context.Database.CreateIfNotExists();

            // Look for any member
            if (context.Members.Any())
            {
                return; // DB has been seeded
            }

            // Create members
            Member[] members = new Member[]
            {
                new Member { FullName = "Allex Rocha", Email = "allexmmr@gmail.com", Code = Guid.NewGuid().ToString(), Status = StatusEnum.Activated, IsAdmin = true },
                new Member { FullName = "Mohsen Kokabi", Email = "mkokabi@developmentbeyondlearning.com", Code = Guid.NewGuid().ToString(), Status = StatusEnum.Pending, IsAdmin = false }
            };

            foreach (Member item in members)
            {
                context.Members.Add(item);
            }

            // Create teams
            Team[] teams = new Team[]
            {
                new Team { Name = "HR" },
                new Team { Name = "IT" }
            };

            foreach (Team item in teams)
            {
                context.Teams.Add(item);
            }

            // Assign members to the teams
            TeamMember[] teamMembers = new TeamMember[]
            {
                new TeamMember { TeamId = 2 /* IT */, MemberId = 1 /* Allex Rocha */ }
            };

            foreach (TeamMember item in teamMembers)
            {
                context.TeamMembers.Add(item);
            }

            context.SaveChanges();
        }
    }
}