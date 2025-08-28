using EmployeeAdminPortal.Controllers;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeeAdminPortal.UnitTests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly EmployeesController _controller;
        public EmployeeControllerTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _controller = new EmployeesController(_employeeServiceMock.Object);
        }
        [Fact]
        public async Task GetEmployees_ReturnsOkResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<EmployeeDTO>
            {
                new EmployeeDTO { Id = Guid.NewGuid(), Name = "John Doe", Email = "test@email.com", Phone = "1234567890", salary = 50000 }
            };

            _employeeServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(employees);
            // Act

            var result = await _controller.GetEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task GetEmployeeById_ReturnsOkResult_WithEmployee()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new EmployeeDTO
            {
                Id = employeeId,
                Name = "John Doe",
                Email = "test1@email.com",
                Phone = "1234567890",
                salary = 90000
            };
            _employeeServiceMock.Setup(service => service.GetByIdAsync(employeeId)).ReturnsAsync(employee);

            // Act
            var result = await _controller.GetEmployeeById(employeeId);
            // Assert

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedEmployee = Assert.IsType<EmployeeDTO>(okResult.Value);
        }

        [Fact]

        public async Task GetEmployeeById_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _employeeServiceMock.Setup(service => service.GetByIdAsync(employeeId)).ReturnsAsync((EmployeeDTO?)null);
            // Act
            var result = await _controller.GetEmployeeById(employeeId);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]

        public async Task AddEmployees_ReturnsCreatedAtAction_WhenEmployeeIsAdded()
        {
            // Arrange
            var employee = new EmployeeDTO
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "test1@email.com",
                Phone = "1234567890",
                salary = 90000
            };

            var addEmployeeDTO = new AddEmployeeDTO
            {
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                salary = employee.salary
            };

            _employeeServiceMock.Setup(service => service.CreateAsync(addEmployeeDTO)).ReturnsAsync(employee);

            // Act

            var result = await _controller.AddEmployees(addEmployeeDTO);
            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            var returnedEmployee = Assert.IsType<EmployeeDTO>(createdAtActionResult.Value);

            Assert.Equal(employee.Id, returnedEmployee.Id);

            Assert.Equal(employee.Name, returnedEmployee.Name);

            Assert.Equal(employee.Email, returnedEmployee.Email);

        }


        [Fact]
        public async Task AddEmployees_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var employee = new EmployeeDTO
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "test1@email.com",
                Phone = "1234567890",
                salary = 90000
            };

            var addEmployeeDTO = new AddEmployeeDTO
            {
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                salary = employee.salary
            };


             _controller.ModelState.AddModelError("Name", "Name is required");
            // Act
            var result = await _controller.AddEmployees(addEmployeeDTO);

            // Assert

            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]

        public async Task UpdateEmployee_ReturnsOk_WhenEmployeeIsUpdated()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            // Arrange
            var employee = new EmployeeDTO
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "test1@email.com",
                Phone = "1234567890",
                salary = 90000
            };


            var updateEmployeeDTO = new UpdateEmploeeDTO
            {
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                salary = employee.salary
            };
            _employeeServiceMock.Setup(service => service.UpdateAsync(employeeId, updateEmployeeDTO)).ReturnsAsync(true);
            // Act
            var result = await _controller.UpdateEmployee(employeeId, updateEmployeeDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Employee updated successfully", okResult.Value);
           
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            // Arrange
            var employee = new EmployeeDTO
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "test1@email.com",
                Phone = "1234567890",
                salary = 90000
            };

            var updateEmployeeDTO = new UpdateEmploeeDTO
            {
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                salary = employee.salary
            };

            _employeeServiceMock.Setup(service => service.UpdateAsync(employeeId, updateEmployeeDTO)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateEmployee(employeeId, updateEmployeeDTO);
            // Assert

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task DeleteEmployee_ReturnsOk_WhenEmployeeIsDeleted()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _employeeServiceMock.Setup(service => service.DeleteAsync(employeeId)).ReturnsAsync(true);
            // Act
            var result = await _controller.DeleteEmployee(employeeId);
            // Assert
           var okResult = Assert.IsType<OkObjectResult>(result);
           Assert.Equal("deleted", okResult.Value);
        }
        [Fact]

        public async Task DeleteEmployee_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _employeeServiceMock.Setup(service => service.DeleteAsync(employeeId)).ReturnsAsync(false);
            // Act
            var result = await _controller.DeleteEmployee(employeeId);
            // Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}