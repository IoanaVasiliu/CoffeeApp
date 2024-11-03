using System.ComponentModel.DataAnnotations;

namespace LibCafeApp.Model
{
    public class OrderStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; }

        // Navigation property
        public virtual ICollection<Order> Orders { get; set; }
    }
}
