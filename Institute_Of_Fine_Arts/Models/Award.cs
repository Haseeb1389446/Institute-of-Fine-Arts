using System.ComponentModel.DataAnnotations;

namespace Institute_Of_Fine_Arts.Models
{
    public class Award
    {
        public int Id { get; set; }

        [Required]
        public int CompetitionId { get; set; }

        [Required]
        public string? StudentId { get; set; }

        [Required]
        public string? AwardTitle { get; set; }

        public string? Remarks { get; set; }

        public DateTime AwardedDate { get; set; }

        public Competition Competition { get; set; }
    }
}
