using System.ComponentModel.DataAnnotations;

namespace LibCafeApp.Model
{
    public class BookReservation
    {
        [Key]
        public int ReservationId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public DateTime ReservationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public bool Stock { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
