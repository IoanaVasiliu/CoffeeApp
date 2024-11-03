using System.ComponentModel.DataAnnotations;

namespace LibCafeApp.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(100)]
        public string Genre { get; set; }

        public int StockQuantity { get; set; }

        public bool Availability { get; set; }

        public virtual ICollection<BookReservation> BookReservations { get; set; }
    }
}
