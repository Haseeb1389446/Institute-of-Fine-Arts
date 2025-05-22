using static Institute_Of_Fine_Arts.Models.Painting;
using System.ComponentModel.DataAnnotations;

namespace Institute_Of_Fine_Arts.Models
{
    public class ExhibitedPainting
    {
        public int Id { get; set; }

        [Required]
        public int PostingId { get; set; }

        public int ExhibitionId { get; set; }

        [Required]
        public decimal PriceQuoted { get; set; }

        public bool IsSold { get; set; }

        public decimal? SoldPrice { get; set; }

        public bool IsPaidToStudent { get; set; }

        public string? BuyerDetails { get; set; }

        public Painting painting { get; set; }

        public Exhibition Exhibition { get; set; }
    }
}
