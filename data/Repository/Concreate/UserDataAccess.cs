using DataAccess.MSettings;
using DataAccess.Repository.Abstract;
using Microsoft.Extensions.Options;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Concreate
{
    public class UserDataAccess : MongoRepository<User>, IUserDataAccess
    {
        public UserDataAccess(IOptions<MongoSettings> settings) : base(settings)
        {
        }
    }
}
