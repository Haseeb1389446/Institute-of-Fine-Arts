using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Institute_Of_Fine_Arts.Models
{
    public class Award
    {
        public int Id { get; set; }

        [Required]
        public string? AwardTitle { get; set; }

        [Required]
        public DateTime AwardedDate { get; set; }

        public string? StudentId { get; set; }

        public IdentityUser? Student { get; set; }
    }
}
