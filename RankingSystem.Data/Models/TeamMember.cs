using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankingSystem.Data.Models
{
    public class TeamMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foreign key for Team
        [Required]
        public short TeamId { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        // Foreign key for Member
        [Required]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }
    }
}