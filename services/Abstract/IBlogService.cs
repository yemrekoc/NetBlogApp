using DataAccess.MSettings;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IBlogService
    {
        Task<GetManyResult<Blog>> GetAllBlogsAsync();
        Task<GetOneResult<Blog>> GetOneBlogAsync(string id);
        Task<GetOneResult<Blog>> InsertOneAsync(Blog data);
        Task<GetOneResult<Blog>> UpdateOneAsync(string id,Blog data);
        Task<GetOneResult<Blog>> DeleteOneAsync(string id);
    }
}
