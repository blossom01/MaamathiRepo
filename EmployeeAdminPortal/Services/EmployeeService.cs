using AutoMapper;
using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<EmployeeDTO>> GetAllAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return _mapper.Map<List<EmployeeDTO>>(employees);

        }
        public async Task<EmployeeDTO?> GetByIdAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeDTO>(employee); 
        }

        public async Task<EmployeeDTO> CreateAsync(AddEmployeeDTO dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeDTO>(employee);
        }
       
        public async Task<bool> UpdateAsync(Guid id, UpdateEmploeeDTO dto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            _mapper.Map(dto, employee);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
