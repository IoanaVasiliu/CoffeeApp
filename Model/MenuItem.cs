using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibCafeApp.Model
{
    public class MenuItem
    {

        [Key]
        public int ItemId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool Availability { get; set; }

        public int Stock { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
