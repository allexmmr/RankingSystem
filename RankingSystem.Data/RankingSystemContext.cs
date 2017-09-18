using RankingSystem.Data.Models;
using System.Data.Entity;

namespace RankingSystem.Data
{
    public class RankingSystemContext : DbContext
    {
        public RankingSystemContext(string connString) : base(connString)
        {
        }

        public DbSet<Member> Members { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamMember> TeamMembers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<TeamMember>().ToTable("TeamMember");
        }
    }
}