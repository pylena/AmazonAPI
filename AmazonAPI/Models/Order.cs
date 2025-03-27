using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AmazonAPI.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        [Required]
        public Guid UserID { get; set; } // FK to User

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")] 
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
