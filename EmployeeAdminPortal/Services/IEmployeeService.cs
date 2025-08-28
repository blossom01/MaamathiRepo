using EmployeeAdminPortal.Models;
using Microsoft.Extensions.Caching.Hybrid;

namespace EmployeeAdminPortal.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllAsync();
        Task<EmployeeDTO?> GetByIdAsync(Guid id);
        Task<EmployeeDTO> CreateAsync(AddEmployeeDTO dto);
        Task<bool> UpdateAsync(Guid id, UpdateEmploeeDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}