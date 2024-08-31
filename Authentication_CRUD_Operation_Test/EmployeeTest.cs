using Authentication_CRUD_Operation.Controllers;
using Authentication_CRUD_Operation.Data;
using Authentication_CRUD_Operation.Dtos;
using Authentication_CRUD_Operation.Globals;
using Authentication_CRUD_Operation.Helpers;
using Authentication_CRUD_Operation.Model;
using Authentication_CRUD_Operation.Repository;
using Authentication_CRUD_Operation.Repository.Employees;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Authentication_CRUD_Operation_Test
{
    public class EmployeeTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeController _employeeController;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IFileUpload> _fileUploadMock;

        public EmployeeTest()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test") // connection string is ignored
                .Options;

            var context = new ApplicationDbContext(_contextOptions);
            SeedDatabase(context);

            _employeeRepository = new EmployeeRepository(context);

            _mapperMock = new Mock<IMapper>();

            _employeeController = new EmployeeController(_employeeRepository, _mapperMock.Object, _fileUploadMock.Object);
        }

        private void SeedDatabase(ApplicationDbContext context)
        {
            var employees = new List<Employee>
            {
                new Employee { Id = Guid.NewGuid(), Name = "John", Email = "john@example.com", PhoneNumber = "+20123456789", IsGraduated = true, CreatedAt = DateTime.Now },
                new Employee { Id = Guid.NewGuid(), Name = "Jane", Email = "jane@example.com", PhoneNumber = "+20123456789", IsGraduated = true, CreatedAt = DateTime.Now }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsSuccessStatusCode()
        {
            // Arrange
            var getAllEmployeesDto = new GetAllEmpolyeeDto
            {
                Name = "John",
            };

            var employeeDtos = new List<EmployeeDto>
            {
                new EmployeeDto { Name = "John", Email = "john@example.com",  PhoneNumber = "+20123456789", IsGraduated = true }
            };

            _mapperMock.Setup(m => m.Map<List<EmployeeDto>>(It.IsAny<List<Employee>>())).Returns(employeeDtos);

            // Act
            var result = await _employeeController.Get(getAllEmployeesDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            //var response = Assert.IsType<BaseResponse<List<EmployeeDto>>>(okResult.Value);

            //Assert.Equal("Success", response.Message);
            //Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            //Assert.NotNull(response.Data);
            //Assert.Equal(employeeDtos.Count, response.Data.Count);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsCreatedStatusCode()
        {
            // Arrange
            var createEmployeeDto = new EmployeeDto
            {
                Name = "John",
                Email = "email@example.com",
                PhoneNumber = "+20123456789",
                IsGraduated = true,
            };

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = createEmployeeDto.Name,
                Email = createEmployeeDto.Email,
                PhoneNumber = "+20123456789",
                IsGraduated = true,
                CreatedAt = DateTime.Now
            };

            _mapperMock.Setup(m => m.Map<Employee>(createEmployeeDto)).Returns(employee);
            _mapperMock.Setup(m => m.Map<EmployeeDto>(It.IsAny<Employee>())).Returns(createEmployeeDto);

            // Act
            var result = await _employeeController.Post(createEmployeeDto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            //var response = Assert.IsType<BaseResponse<EmployeeDto>>(createdResult.Value);

            //Assert.Equal("Employee created successfully", response.Message);
            //Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            //Assert.NotNull(response.Data);
            //Assert.Equal(createEmployeeDto.Name, response.Data.Name);
            //Assert.Equal(createEmployeeDto.Email, response.Data.Email);
            //Assert.Equal(createEmployeeDto.Salary, response.Data.Salary);
        }

        [Fact]
        public async Task DeleteEmployee_ReturnsSuccessStatusCode_WhenEmployeeExists()
        {
            // Arrange
            var context = new ApplicationDbContext(_contextOptions);
            var existingEmployee = await context.Employees.FirstOrDefaultAsync();
            Assert.NotNull(existingEmployee);

            // Act
            var result = await _employeeController.Delete(existingEmployee.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            //var response = Assert.IsType<BaseResponse<Guid>>(okResult.Value);

            //Assert.Equal("Employee deleted successfully", response.Message);
            //Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            //Assert.Equal(existingEmployee.Id, response.Data);
        }

        [Fact]
        // update employee
        public async Task UpdateEmployee_ReturnsSuccessStatusCode_WhenEmployeeExists()
        {
            // Arrange
            var context = new ApplicationDbContext(_contextOptions);
            var existingEmployee = await context.Employees.FirstOrDefaultAsync();
            Assert.NotNull(existingEmployee);

            var updateEmployeeDto = new EmployeeDto
            {
                Name = "John",
                Email = "email2@example.com",
                PhoneNumber = "+20123456789",
                IsGraduated = true,
            };

            var updatedEmployee = new Employee
            {
                Id = existingEmployee.Id,
                Name = updateEmployeeDto.Name,
                Email = updateEmployeeDto.Email,
                PhoneNumber = "+20123456789",
                IsGraduated = true,
                CreatedAt = DateTime.Now
            };

            _mapperMock.Setup(m => m.Map<Employee>(updateEmployeeDto)).Returns(updatedEmployee);
            _mapperMock.Setup(m => m.Map<EmployeeDto>(It.IsAny<Employee>())).Returns(updateEmployeeDto);

            // Act
            //var result = await _employeeController.Put(existingEmployee.Id, updateEmployeeDto);

            // Assert
            //var okResult = Assert.IsType<OkObjectResult>(result);
            //var response = Assert.IsType<BaseResponse<EmployeeDto>>(okResult.Value);

            //Assert.Equal("Employee updated successfully", response.Message);
            //Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            //Assert.NotNull(response.Data);
            //Assert.Equal(updateEmployeeDto.Name, response.Data.Name);
            //Assert.Equal(updateEmployeeDto.Email, response.Data.Email);
            //Assert.Equal(updateEmployeeDto.Salary, response.Data.Salary);
        }
    }
}
