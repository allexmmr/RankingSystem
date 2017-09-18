using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankingSystem.Data;
using RankingSystem.Data.Models;
using System.Data.Entity;

namespace RankingSystem.Test
{
    [TestClass]
    public class TeamClass
    {
        private readonly RankingSystemContext _context;

        public TeamClass()
        {
            RankingSystemContextFactory factory = new RankingSystemContextFactory();
            _context = new RankingSystemContext(factory.Create().Database.Connection.ConnectionString);
        }

        private static readonly Team _team = new Team { Name = "My Team" };

        private const int _id = 1;

        [TestMethod]
        public void List()
        {
            var actual = _context.Teams.ToListAsync();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Result.Count >= 0);
        }

        [TestMethod]
        public void Details()
        {
            var actual = _context.Teams.SingleOrDefaultAsync(q => q.Id == _id);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Result.Id >= 0);
        }

        [TestMethod]
        public void Create()
        {
            var actual = _context.Teams.Add(_team);
            _context.SaveChangesAsync();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Id >= 0);
        }

        [TestMethod]
        public void Edit()
        {
            var actual = _context.Teams.Attach(_team);
            _context.Entry(_team).State = EntityState.Modified;
            _context.SaveChangesAsync();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Id >= 0);
        }

        [TestMethod]
        public void Delete()
        {
            var team = _context.Teams.SingleOrDefaultAsync(q => q.Id == _id).Result;
            _context.Teams.Remove(team);
            var actual = _context.SaveChangesAsync();

            Assert.IsTrue(actual.Result != 0);
        }
    }
}