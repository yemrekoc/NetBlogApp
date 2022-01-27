using DataAccess.MSettings;
using DataAccess.Repository.Abstract;
using DataAccess.Repository.Concreate;
using Models.Entities;
using MongoDB.Bson;
using Services.Abstract;
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

        public BlogService(IBlogDataAccess blogDataAccess)
        {
            this.blogDataAccess = blogDataAccess;
        }

        public async Task<GetManyResult<Blog>> GetAllBlogsAsync()
        {
            var list = await blogDataAccess.GetAllAsync();

            return list;
        }

        public async Task<GetOneResult<Blog>> GetOneBlogAsync(string id)
        {
            var result = await blogDataAccess.GetByIdAsync(id);
            result.Message = "Bir blog getirildi";
            return result;
        }

        public async Task<GetOneResult<Blog>> InsertOneAsync(Blog data)
        {
            var result = await blogDataAccess.InsertOneAsync(data);
            result.Message = "Bir blog kaydedildi";
            return result;
        }

        public async Task<GetOneResult<Blog>> UpdateOneAsync(string id, Blog updateData)
        {
            var data =  await blogDataAccess.GetByIdAsync(id);
            data.Entity.Title = updateData.Title;
            data.Entity.comSentence = updateData.comSentence;
            data.Entity.comImage = updateData.comImage;
            data.Entity.BlogText = updateData.BlogText;
            data.Entity.Title = updateData.Title;

            var result = await blogDataAccess.ReplaceOneAsync(data.Entity, id);
            result.Message = "Bir blog düzenlendi";
            return result;
        }

        public async Task<GetOneResult<Blog>> DeleteOneAsync(string id)
        {
            var result = await blogDataAccess.DeleteByIdAsync(id);
            result.Message = "Bir blog silindi";
            return result;
        }
    }
}
