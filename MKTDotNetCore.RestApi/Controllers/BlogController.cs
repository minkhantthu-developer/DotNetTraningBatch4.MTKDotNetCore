
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKTDotNetCore.RestApi.Db;
using MKTDotNetCore.RestApi.Models;

namespace MKTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogController()
        {
            _db = new AppDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lst = _db.blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _db.blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound("No data found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post(BlogModel blog)
        {
            _db.blogs.Add(blog);
            string message = _db.SaveChanges() > 0 ? "Successfully Save" : "Fail to save";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,BlogModel blog)
        {
            var item = _db.blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound("No data found");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor=blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            string message = _db.SaveChanges() > 0 ? "Successfully Update" : "Update Fail";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id,BlogModel blog)
        {
            var item = _db.blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found");
            }
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogTitle = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogTitle = blog.BlogContent;
            }
            string message = _db.SaveChanges() > 0 ? "Successfully Update" : "Update Fail";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _db.blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found");
            }
            _db.blogs.Remove(item);
            string message = _db.SaveChanges() > 0 ? "Successfully Delete" : "Delete Fail";
            return Ok(message);
        }
    }
}
