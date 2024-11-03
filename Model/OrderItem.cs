using System.ComponentModel.DataAnnotations.Schema;

namespace LibCafeApp.Model
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int MenuItemId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual MenuItem MenuItem { get; set; }
    }
}
