namespace EmployeeAdminPortal.Models
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public decimal salary { get; set; }
    }
}
