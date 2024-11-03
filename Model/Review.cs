using System.ComponentModel.DataAnnotations;

namespace LibCafeApp.Model
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }
}
