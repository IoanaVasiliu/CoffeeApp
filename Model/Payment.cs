using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibCafeApp.Model
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int OrderId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        // Navigation property
        public virtual Order Order { get; set; }
    }
}
