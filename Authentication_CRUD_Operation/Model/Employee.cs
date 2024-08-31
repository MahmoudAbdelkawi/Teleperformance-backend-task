using System.ComponentModel.DataAnnotations;

namespace Authentication_CRUD_Operation.Model
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string? ProfileImage { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsGraduated { get; set; }
    }
}
