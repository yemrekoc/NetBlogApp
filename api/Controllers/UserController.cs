using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Shared.DTO;
using Shared.ResponseModels;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService UserService)
        {
            userService = UserService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ServiceResponse<UserLoginResponseDTO>> Login(UserLoginRequestDTO UserRequest)
        {
            return new ServiceResponse<UserLoginResponseDTO>()
            {
                Value = await userService.LoginAsync(UserRequest.Email, UserRequest.Password)
            };
        }

        [HttpGet("GetAllUsers")]
        public async Task<ServiceResponse<List<UserDTO>>> GetAllUsers()
        {
            return new ServiceResponse<List<UserDTO>>()
            {
                Value = await userService.GetUsersAsync()
            };
        }

        [HttpPost("CreateOneUser")]
        public async Task<ServiceResponse<UserDTO>> CreateUser([FromBody] UserDTO User)
        {
            return new ServiceResponse<UserDTO>()
            {
                Value = await userService.CreateUserAsync(User)
            };
        }

        [HttpPost("UpdateOneUser")]
        public async Task<ServiceResponse<UserDTO>> UpdateUser([FromBody] UserDTO User)
        {
            return new ServiceResponse<UserDTO>()
            {
                Value = await userService.UpdateUserAsync(User, Convert.ToString(User.Id))
            };
        }

        [HttpGet("GetOneUserById/{Id}")]
        public async Task<ServiceResponse<UserDTO>> GetUserById(string Id)
        {
            return new ServiceResponse<UserDTO>()
            {
                Value = await userService.GetUserByIdAsync(Id)
            };
        }


        [HttpPost("DeleteOneUser")]
        public async Task<ServiceResponse<UserDTO>> DeleteUser([FromBody] string id)
        {
            return new ServiceResponse<UserDTO>()
            {
                Value = await userService.DeleteUserByIdAsync(id)
            };
        }
    }
}
