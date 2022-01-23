using DataAccess.MSettings;
using DataAccess.Repository.Abstract;
using DataAccess.Repository.Concreate;
using Models.Entities;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BlogServices
{
    public class BlogService :IBlogService
    {
        private readonly IBlogDataAccess blogDataAccess;

        public BlogService(IBlogDataAccess blogDataAccess)
        {
            this.blogDataAccess = blogDataAccess;
        }

        public async Task<GetManyResult<Blog>> GetAllBlogsAsync()
        {
            var list = await blogDataAccess.GetAllAsync();

            return list;
        }
    }
}
