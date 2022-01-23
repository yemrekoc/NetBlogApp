using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Models.Entities;
using MongoDB.Bson;
using Services.BlogServices;
using Services.Abstract;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : Controller
    {
        private readonly MongoClient client;
        private readonly IBlogService blogService;
        

        public BlogController(IBlogService blogService)
        {
            //client = new MongoClient("mongodb://yekdbuser:VumJi8QL2wYKEeV9@yekcluster-shard-00-00.0duvw.mongodb.net:27017,yekcluster-shard-00-01.0duvw.mongodb.net:27017,yekcluster-shard-00-02.0duvw.mongodb.net:27017/CarParkDB?ssl=true&replicaSet=yekcluster-shard-0&authSource=admin&retryWrites=true&w=majority");
            client = new MongoClient("mongodb://127.0.0.1:27017/");
            this.blogService = blogService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var database = client.GetDatabase("BlogAppDb");
            //var collection = database.GetCollection<Blog>("Blog");

            //var Blog = new Blog()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    BlogText = "test",
            //    comImage = "test",
            //    Title = "test"
            //};
            //collection.InsertOne(Blog);

            return Ok( await blogService.GetAllBlogsAsync());
        }

        //[HttpGet]
        //public IActionResult GetAllBlogs()
        //{
        //    return Ok(blogService.GetAllBlogs());
        //}
    }
}
