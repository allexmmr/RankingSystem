using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankingSystem.Data;
using RankingSystem.Data.Models;
using System;
using System.Data.Entity;
using static RankingSystem.Common.Domain.Enums;

namespace RankingSystem.Test
{
    [TestClass]
    public class MemberClass
    {
        private readonly RankingSystemContext _context;

        public MemberClass()
        {
            RankingSystemContextFactory factory = new RankingSystemContextFactory();
            _context = new RankingSystemContext(factory.Create().Database.Connection.ConnectionString);
        }

        private static readonly Member _memberCreate =
            new Member { FullName = "Admin", Email = "admin@test.com", Code = Guid.NewGuid().ToString(), Status = StatusEnum.Activated, IsAdmin = true };

        private static readonly Member _memberEdit =
            new Member { FullName = "Member", Email = "member@test.com", Code = Guid.NewGuid().ToString(), Status = StatusEnum.Pending, IsAdmin = false };

        private const int _id = 2;

        [TestMethod]
        public void List()
        {
            var actual = _context.Members.ToListAsync();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Result.Count >= 0);
        }

        [TestMethod]
        public void Details()
        {
            var actual = _context.Members.SingleOrDefaultAsync(q => q.Id == _id);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Result.Id >= 0);
        }

        [TestMethod]
        public void Create()
        {
            var actual = _context.Members.Add(_memberCreate);
            _context.SaveChangesAsync();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Id >= 0);
        }

        [TestMethod]
        public void Edit()
        {
            var actual = _context.Members.Attach(_memberEdit);
            _context.Entry(_memberEdit).State = EntityState.Modified;
            _context.SaveChangesAsync();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Id >= 0);
        }

        [TestMethod]
        public void Delete()
        {
            var member = _context.Members.SingleOrDefaultAsync(q => q.Id == _id).Result;
            _context.Members.Remove(member);
            var actual = _context.SaveChangesAsync();

            Assert.IsTrue(actual.Result != 0);
        }
    }
}