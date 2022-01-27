using AutoMapper;
using DataAccess.Repository.Abstract;
using DataAccess.MSettings;
using Services.Abstract;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;
using Shared.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserDataAccess userDataAccess;
        private readonly IConfiguration configuration;


        public UserService(IUserDataAccess userDataAccess, IConfiguration configuration , IMapper mapper)
        {
            this.userDataAccess = userDataAccess;
            this.configuration = configuration;
            this.mapper = mapper;
        }


        public async Task<UserDTO> CreateUserAsync(UserDTO User)
        {
            var res = await userDataAccess.GetOneFilterByAsync(x=>x.EMailAddress == User.EMailAddress);

            if (res.Entity != null)
                throw new Exception("User already exists");

            var addUser = mapper.Map<User>(User);

            var data =  await userDataAccess.InsertOneAsync(addUser);

            var rest = mapper.Map<UserDTO>(data.Entity);

            return mapper.Map<UserDTO>(data.Entity);
        }

        public async Task<UserDTO> DeleteUserByIdAsync(string Id)
        {

            var res = await userDataAccess.DeleteByIdAsync(Id);
            if (res.Success!)
                throw new Exception("User already exists");
            var data = mapper.Map<UserDTO>(res.Entity);

            return data;
        }

        public async Task<UserDTO> GetUserByIdAsync(string Id)
        {
            var res = await userDataAccess.GetByIdAsync(Id);

            var data = mapper.Map<UserDTO>(res.Entity);

            return data;
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var res = await userDataAccess.FilterByAsync(x =>x.IsActive);

            var list = res.Result;

            var list2 = mapper.ProjectTo<UserDTO>((IQueryable)list);

            return list2.ToList();
        }

        public async Task<UserLoginResponseDTO> LoginAsync(string EMail, string Password)
        {
            // Veritabanı Kullanıcı Doğrulama İşlemleri Yapıldı.

            var encryptedPassword = PasswordEncrypter.Encrypt(Password);

            var getOneResult = await userDataAccess.GetOneFilterByAsync(i => i.EMailAddress == EMail && i.Password == encryptedPassword);

            var dbUser = getOneResult.Entity;

            if (dbUser == null)
                throw new Exception("User not found or given information is wrong");

            if (!dbUser.IsActive)
                throw new Exception("User state is Passive!");


            UserLoginResponseDTO result = new UserLoginResponseDTO();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(int.Parse(configuration["JwtExpiryInDays"].ToString()));

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, EMail),
                new Claim(ClaimTypes.Name, dbUser.FirstName + " " + dbUser.LastName),
                new Claim(ClaimTypes.UserData, dbUser.Id.ToString())
            };

            var token = new JwtSecurityToken(configuration["JwtIssuer"], configuration["JwtAudience"], claims, null, expiry, creds);

            result.ApiToken = new JwtSecurityTokenHandler().WriteToken(token);
            result.User = mapper.Map<UserDTO>(dbUser);

            return result;
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO User,string id)
        {
            var dbUser = await userDataAccess.GetByIdAsync(id);

            if (dbUser == null)
                throw new Exception("User not found");


            mapper.Map(User, dbUser);

            var result = userDataAccess.ReplaceOneAsync(dbUser.Entity, id);

            return mapper.Map<UserDTO>(dbUser);
        }


    }
}

