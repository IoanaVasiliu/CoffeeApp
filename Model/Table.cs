using System.ComponentModel.DataAnnotations;

namespace LibCafeApp.Model
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        public int Number { get; set; }

        public int Capacity { get; set; }

        public bool Availability { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
