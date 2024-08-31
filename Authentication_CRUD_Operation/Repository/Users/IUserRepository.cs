using Authentication_CRUD_Operation.Dtos;
using Authentication_CRUD_Operation.Globals;
using Microsoft.AspNetCore.Identity;

namespace Authentication_CRUD_Operation.Repository.Users
{
    public interface IUserRepository
    {
        public Task<BaseResponse<string>> Signup(IdentityUser user, string password, string role);
        public Task<BaseResponse<string>> Login(string email, string password);
        public Task<IdentityUser> GetCurrentUser();
    }
}
