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
    public interface IBlogService
    {
        Task<List<BlogDTO>> GetAllBlogsAsync();
        Task<BlogDTO> GetOneBlogAsync(string id);
        Task<BlogDTO> InsertOneAsync(BlogDTO data);
        Task<BlogDTO> UpdateOneAsync(string id,BlogDTO data);
        Task<BlogDTO> DeleteOneAsync(string id);
    }
}
