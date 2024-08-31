using System.ComponentModel.DataAnnotations;

namespace Authentication_CRUD_Operation.Dtos
{
    public class GetAllEmpolyeeDto
    {
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsGraduated { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set;} = 1;
    }
}
