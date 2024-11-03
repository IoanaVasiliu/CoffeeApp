using System.ComponentModel.DataAnnotations;

namespace LibCafeApp.Model
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        public int UserId { get; set; }

        public int TableId { get; set; }

        public DateTime ReservationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Table Table { get; set; }
    }
}
