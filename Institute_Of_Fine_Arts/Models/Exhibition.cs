using System.ComponentModel.DataAnnotations;

namespace Institute_Of_Fine_Arts.Models
{
    public class Exhibition
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime ExhibitionDate { get; set; }

        public ICollection<ExhibitedPainting> ExhibitedPaintings { get; set; }
    }
}
