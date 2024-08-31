using Authentication_CRUD_Operation.Dtos;
using Authentication_CRUD_Operation.enums;
using Authentication_CRUD_Operation.Globals;
using Authentication_CRUD_Operation.Repository.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Authentication_CRUD_Operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserDto userDto)
        {
            var user = _mapper.Map<IdentityUser>(userDto);
            var result = await _userRepository.Signup(user, userDto.Password ,userDto.Role.ToString());
            return Result(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            var result = await _userRepository.Login(userDto.Email, userDto.Password);
            return Result(result);
        }

        [HttpGet("me")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userRepository.GetCurrentUser();
            return Result(new BaseResponse<UserDto>
            {
                Data = _mapper.Map<UserDto>(user),
                Message = "User found",
                StatusCode = HttpStatusCode.OK
            });
        }
    }
}
