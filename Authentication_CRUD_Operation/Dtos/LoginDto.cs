using System.ComponentModel.DataAnnotations;

namespace Authentication_CRUD_Operation.Dtos
{
    public class LoginDto
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
