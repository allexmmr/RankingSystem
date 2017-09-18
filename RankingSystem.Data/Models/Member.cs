using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RankingSystem.Common.Domain.Enums;

namespace RankingSystem.Data.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Full Name field cannot be longer than 100 characters.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The Email field cannot be longer than 50 characters.")]
        public string Email { get; set; }

        public string Code { get; set; }

        public StatusEnum Status { get; set; }

        [Display(Name = "Is Admin?")]
        public bool IsAdmin { get; set; }
    }
}