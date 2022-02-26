using AutoMapper;
using DataAccess.MSettings;
using DataAccess.Repository.Abstract;
using DataAccess.Repository.Concreate;
using Models.Entities;
using MongoDB.Bson;
using Services.Abstract;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly IBlogDataAccess blogDataAccess;
        private readonly IMapper mapper;

        public BlogService(IBlogDataAccess blogDataAccess,IMapper mapper)
        {
            this.blogDataAccess = blogDataAccess;
            this.mapper = mapper;
        }

        public async Task<List<BlogDTO>> GetAllBlogsAsync()
        {
            var list = await blogDataAccess.GetAllAsync();

            var res = mapper.Map<List<BlogDTO>>(list.Result);

            return res.ToList();
        }

        public async Task<BlogDTO> GetOneBlogAsync(string id)
        {
            var data = await blogDataAccess.GetByIdAsync(id);
            var result = mapper.Map<BlogDTO>(data);
            return result;
        }

        public async Task<BlogDTO> InsertOneAsync(BlogDTO blog)
        {
            var insertData = mapper.Map<Blog>(blog);
            var data = await blogDataAccess.InsertOneAsync(insertData);
            var result =  mapper.Map<BlogDTO>(data);
            return result;
        }

        public async Task<BlogDTO> UpdateOneAsync(string id, BlogDTO updateData)
        {
            var data =  await blogDataAccess.GetByIdAsync(id);
            data.Entity.Title = updateData.Title;
            data.Entity.comSentence = updateData.comSentence;
            data.Entity.comImage = updateData.comImage;
            data.Entity.BlogText = updateData.BlogText;
            data.Entity.Title = updateData.Title;

            var result = await blogDataAccess.ReplaceOneAsync(data.Entity, id);
            var res = mapper.Map<BlogDTO>(result);
            return res;
        }

        public async Task<BlogDTO> DeleteOneAsync(string id)
        {
            var data = await blogDataAccess.DeleteByIdAsync(id);
            var result = mapper.Map<BlogDTO>(data.Entity);
            return result;
        }
    }
}
