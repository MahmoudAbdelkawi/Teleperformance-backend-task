using Authentication_CRUD_Operation.Dtos;
using Authentication_CRUD_Operation.Globals;
using Authentication_CRUD_Operation.Helpers;
using Authentication_CRUD_Operation.Model;
using Authentication_CRUD_Operation.Repository.Employees;
using Authentication_CRUD_Operation.Specifications;
using Authentication_CRUD_Operation.Specifications.Base;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Authentication_CRUD_Operation.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : BaseController
    {
        // Add CRUD Operation for Employee
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper, IFileUpload fileUpload)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllEmpolyeeDto getAllEmpolyeeDto)
        {
            var employees = _employeeRepository.GetAllAsync();
            var empSpec = new EmployeeSpec(getAllEmpolyeeDto.Name, getAllEmpolyeeDto.Email, getAllEmpolyeeDto.PhoneNumber, getAllEmpolyeeDto.IsGraduated, getAllEmpolyeeDto.PageSize, getAllEmpolyeeDto.PageNumber);
            var employeesSpec = SpecificationEvaluator<Employee>.GetQueryWithSpec(employees, empSpec);
            return Result(
                new BaseResponse<List<Employee>>
                {
                    Message = "Success",
                    Data = employeesSpec.ToList(),
                    StatusCode = HttpStatusCode.OK
                }
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return Result(
                    new BaseResponse<Employee>
                    {
                        Message = "Employee not found",
                        StatusCode = HttpStatusCode.NotFound
                    }
                    );
            }
            return Result(
                new BaseResponse<Employee>
                {
                    Message = "Success",
                    Data = employee,
                    StatusCode = HttpStatusCode.OK
                }
            );
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] EmployeeDto employee)
        {
            string? extension = Path.GetExtension(employee.UploadImage?.FileName)?.TrimStart('.').ToLower();
            if (extension != "jpeg" && extension != "png" && extension != "jpg" && extension is not null)
            {
                return Result(
                    new BaseResponse<Employee>
                    {
                        Message = "Invalid file format",
                        StatusCode = HttpStatusCode.UnsupportedMediaType
                    });
            }
            var mappedEmployee = _mapper.Map<Employee>(employee);
            var (fileName, _) = _fileUpload.UploadFile(employee.UploadImage, enums.UploadDirectoriesEnum.Employee);
            mappedEmployee.ProfileImage = fileName;
            var newEmployee = await _employeeRepository.CreateAsync(mappedEmployee);
            return Result(
                new BaseResponse<Employee>
                {
                    Message = "Employee created",
                    Data = newEmployee,
                    StatusCode = HttpStatusCode.Created
                }
           );
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] EmployeeDto employee)
        {
            var emp = await _employeeRepository.GetByIdAsync(id);
            if (emp == null)
            {
                return Result(
                    new BaseResponse<Employee>
                    {
                        Message = "Employee not found",
                        StatusCode = HttpStatusCode.NotFound
                    }
                );
            }
            //var mappedEmployee = _mapper.Map<Employee>(employee);
            var mappedEmployee = _mapper.Map(employee, emp);
            var updatedEmployee = await _employeeRepository.UpdateAsync(id, mappedEmployee);
            return Result(
                    new BaseResponse<Employee>
                    {
                        Message = "Employee updated",
                        Data = updatedEmployee,
                        StatusCode = HttpStatusCode.OK
                    }
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return Result(
                                       new BaseResponse<Employee>
                                       {
                        Message = "Employee not found",
                        StatusCode = HttpStatusCode.NotFound
                    }
                                                      );
            }
            var deletedEmployee = await _employeeRepository.DeleteAsync(employee);
            return Result(
                               new BaseResponse<Employee>
                               {
                    Message = "Employee deleted",
                    Data = deletedEmployee,
                    StatusCode = HttpStatusCode.OK
                }
                                          );
        }
    }
}
