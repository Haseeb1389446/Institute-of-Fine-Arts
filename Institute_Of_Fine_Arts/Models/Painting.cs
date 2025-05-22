using System.ComponentModel.DataAnnotations;

namespace Institute_Of_Fine_Arts.Models
{
    public class Painting
    {
            public int Id { get; set; }

            [Required]
            public int CompetitionId { get; set; }

            [Required]
            public string? StudentId { get; set; } // Link with Identity User or Student Table

            [Required]
            public string? DesignFilePath { get; set; }

            public string? Description { get; set; }

            public string? PoemOrQuote { get; set; }

            public DateTime DatePosted { get; set; }

            public string? Mark { get; set; } // best, better, good, etc.

            public string? Remarks { get; set; }

            public Competition Competition { get; set; }
    }
}
