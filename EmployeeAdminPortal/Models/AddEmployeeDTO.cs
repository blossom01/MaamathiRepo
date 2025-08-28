using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models
{
    public class AddEmployeeDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public string? Phone { get; set; }

        public decimal salary { get; set; }
    }
}
