using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonAPI.Models
{
    public class User
    {
        public Guid UserID { get; set; } = Guid.NewGuid(); // Auto generate ids
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Customer

    }
}
