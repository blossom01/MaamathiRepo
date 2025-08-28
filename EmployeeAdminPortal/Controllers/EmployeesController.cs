using AutoMapper;
using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;



namespace EmployeeAdminPortal.Controllers
{   
    //localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        
        private readonly IEmployeeService _employeeService;
       
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            var result = await _employeeService.GetAllAsync();            
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult> GetEmployeeById(Guid id)        {

            var result = await _employeeService.GetByIdAsync(id);
            
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployees(AddEmployeeDTO addEmployeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Name or Email cannot be empty");
            }                
           var employee= await _employeeService.CreateAsync(addEmployeeDTO);
            
           return CreatedAtAction(nameof(GetEmployeeById),  new { id = employee.Id },employee);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateEmployee(Guid id, UpdateEmploeeDTO updateemployeedto)
        {
            var updated = await _employeeService.UpdateAsync(id, updateemployeedto);

            if (!updated)
            {
                return NotFound();
            }

            return Ok("Employee updated successfully");
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            var deleted =  await _employeeService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }
            
            return Ok("deleted");
        }
    }
}
