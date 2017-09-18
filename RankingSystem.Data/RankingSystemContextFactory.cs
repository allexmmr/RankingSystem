using System.Data.Entity.Infrastructure;

namespace RankingSystem.Data
{
    public class RankingSystemContextFactory : IDbContextFactory<RankingSystemContext>
    {
        public RankingSystemContext Create()
        {
            return new RankingSystemContext("Server=(localdb)\\mssqllocaldb;Database=RankingSystem;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}