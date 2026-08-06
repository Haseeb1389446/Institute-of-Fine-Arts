using System.ComponentModel.DataAnnotations;

namespace Institute_Of_Fine_Arts.Models
{
    public class Painting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? StudentId { get; set; }

        [Required]
        public string? PaintingName { get; set; }

        public string? Description { get; set; }

        public string? PoemOrQuote { get; set; }

        public string? CompetitionId { get; set; }

        public string? PaintingImage { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}
