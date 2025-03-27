using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AmazonAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public Guid CreatedBy { get; set; } // FK to User
    }
}
