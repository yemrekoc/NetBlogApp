using DataAccess.MSettings;
using DataAccess.Repository.Abstract;
using Models.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Concreate
{
    public class BlogDataAccess : MongoRepository<Blog>, IBlogDataAccess
    {
        public BlogDataAccess(IOptions<MongoSettings> settings) : base(settings)
        {
        }
    }
}
