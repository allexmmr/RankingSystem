using RankingSystem.Data.Models;
using System.Collections.Generic;

namespace RankingSystem.Web.Models
{
    public class TeamMemberViewModel : TeamMember
    {
        public List<Member> Members { get; set; }
    }
}