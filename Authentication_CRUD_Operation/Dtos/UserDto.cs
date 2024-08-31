using Authentication_CRUD_Operation.enums;
using System.ComponentModel.DataAnnotations;

namespace Authentication_CRUD_Operation.Dtos
{
    public class UserDto
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
