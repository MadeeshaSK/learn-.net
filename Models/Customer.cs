using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone")]
        [StringLength(20)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}