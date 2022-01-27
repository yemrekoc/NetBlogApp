using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Models.Entities;
using MongoDB.Bson;
using Services.Concrete;
using Services.Abstract;
using DataAccess.MSettings;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<GetManyResult<Blog>> GetAllBlogs()
        {
            return await blogService.GetAllBlogsAsync();
        }

        [HttpGet("GetOneBlog/{id}")]
        public async Task<GetOneResult<Blog>> GetOneBlog(string id)
        {
            return await blogService.GetOneBlogAsync(id);
        }


        [HttpPost("InsertOneBlog")]
        public async Task<GetOneResult<Blog>> InsertOneBlog(Blog data)
        {
            return await blogService.InsertOneAsync(data);
        }

        [HttpPut("UpdateOneBlog")]
        public async Task<GetOneResult<Blog>> UpdateOneBlog(string id,Blog data)
        {
            return await blogService.UpdateOneAsync(id,data);
        }

        [HttpDelete("DeleteOneBlog")]
        public async Task<GetOneResult<Blog>> DeleteOneBlog(string id)
        {
            return await blogService.DeleteOneAsync(id);
        }
    }
}
