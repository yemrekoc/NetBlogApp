using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Models.Entities;
using MongoDB.Bson;
using Services.Concrete;
using Services.Abstract;
using DataAccess.MSettings;
using Microsoft.AspNetCore.Authorization;
using Shared.ResponseModels;
using Shared.DTO;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BlogController : Controller
    {
        //private readonly MongoClient client;
        private readonly IBlogService blogService;


        public BlogController(IBlogService blogService)
        {

            this.blogService = blogService;

        }

        [HttpGet("GetAllBlogs")]
        [AllowAnonymous]
        public async Task<ServiceResponse<List<BlogDTO>>> GetAllBlogs()
        {
            return new ServiceResponse<List<BlogDTO>>()
            {
                Value = await blogService.GetAllBlogsAsync()
            };
        }

        [HttpGet("GetOneBlog/{id}")]
        public async Task<ServiceResponse<BlogDTO>> GetOneBlog(string blogId)
        {
            return new ServiceResponse<BlogDTO>()
            {
                Value = await blogService.GetOneBlogAsync(blogId)
            };
        }


        [HttpPost("InsertOneBlog")]
        public async Task<ServiceResponse<BlogDTO>> InsertOneBlog(BlogDTO data)
        {
            return new ServiceResponse<BlogDTO>()
            {
                Value = await blogService.InsertOneAsync(data)
            };
        }

        [HttpPut("UpdateOneBlog")]
        public async Task<ServiceResponse<BlogDTO>> UpdateOneBlog(string id, BlogDTO data)
        {
            return new ServiceResponse<BlogDTO>()
            {
                Value = await blogService.UpdateOneAsync(id,data)
            };
        }

        [HttpDelete("DeleteOneBlog")]
        public async Task<ServiceResponse<BlogDTO>> DeleteOneBlog(string id)
        {
            return new ServiceResponse<BlogDTO>()
            {
                Value = await blogService.DeleteOneAsync(id)
            };
        }
    }
}
