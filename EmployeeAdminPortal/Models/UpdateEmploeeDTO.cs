namespace EmployeeAdminPortal.Models
{
    public class UpdateEmploeeDTO
    {
        public required string Name { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public decimal salary { get; set; }
    }
}
