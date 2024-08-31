using System.ComponentModel.DataAnnotations;

namespace Authentication_CRUD_Operation.Dtos
{
    public class EmployeeDto
    {
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsGraduated { get; set; }
        public IFormFile UploadImage { get; set; }
    }
}
