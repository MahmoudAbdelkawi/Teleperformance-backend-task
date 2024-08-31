using Authentication_CRUD_Operation.Dtos;
using Authentication_CRUD_Operation.Globals;
using Authentication_CRUD_Operation.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Authentication_CRUD_Operation.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWT _jwt;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(UserManager<IdentityUser> userManager, IOptions<JWT> jwt, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityUser> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<BaseResponse<string>> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new BaseResponse<string>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Error in email or password",
                };
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
            {
                return new BaseResponse<string>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Error in email or password",
                };
            }

            var token = await CreateJwtToken(user);

            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Login successful",
                Data = token
            };

        }

        public async Task<BaseResponse<string>> Signup(IdentityUser user, string password, string role)
        {
            var userExists = await _userManager.FindByEmailAsync(user.Email);

            if (userExists != null)
            {
                return new BaseResponse<string>
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Message = "User already exists"
                };
            }

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new BaseResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Error in creating user"
                };
            }

            await _userManager.AddToRoleAsync(user, role);

            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User created successfully",
                Data = await CreateJwtToken(user)
            };
        }

        private async Task<string> CreateJwtToken(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role , roles.FirstOrDefault()),
                new Claim(ClaimTypes.Email , user.Email)
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.ExpiryInMinutes),
                signingCredentials: signingCredentials);


            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
