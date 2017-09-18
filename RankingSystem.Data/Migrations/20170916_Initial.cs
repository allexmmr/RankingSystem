using System.Data.Entity.Migrations;

namespace RankingSystem.Data.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Member",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(),
                    Email = c.String(),
                    Code = c.String(),
                    Status = c.Byte(nullable: false),
                    IsAdmin = c.Boolean(nullable: false)
                }).PrimaryKey(q => q.Id);

            CreateTable(
                "dbo.Team",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String()
                }).PrimaryKey(q => q.Id);

            CreateTable(
                "dbo.TeamMember",
                c => new
                {
                    Id = c.Int(nullable: false),
                    TeamId = c.Short(nullable: false),
                    MemberId = c.Int(nullable: false)
                }).PrimaryKey(q => q.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Member");

            DropTable("dbo.Team");

            DropTable("dbo.TeamMember");
        }
    }
}