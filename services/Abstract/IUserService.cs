using DataAccess.MSettings;
using Models.Entities;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IUserService
    {
        public Task<UserDTO> GetUserByIdAsync(string Id);

        public Task<List<UserDTO>> GetUsersAsync();

        public Task<UserDTO> CreateUserAsync(UserDTO User);

        public Task<UserDTO> UpdateUserAsync(UserDTO User, string id);

        public Task<UserDTO> DeleteUserByIdAsync(string Id);

        public Task<UserLoginResponseDTO> LoginAsync(string EMail, string Password);
    }
}
