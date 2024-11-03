using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibCafeApp.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public int StatusId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
