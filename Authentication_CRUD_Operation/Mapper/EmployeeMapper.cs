using Authentication_CRUD_Operation.Dtos;
using Authentication_CRUD_Operation.Model;
using AutoMapper;

namespace Authentication_CRUD_Operation.Mapper
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
