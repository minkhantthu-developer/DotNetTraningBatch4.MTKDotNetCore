
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetBlogs()
        {
            var lst = await _db.blogs.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var item = await _db.blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found");
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BlogModel blog)
        {
            _db.blogs.Add(blog);
            string message = await _db.SaveChangesAsync() > 0 ? "Successfully Save" : "Fail to save";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BlogModel blog)
        {
            var item = await _db.blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            string message = await _db.SaveChangesAsync() > 0 ? "Successfully Update" : "Update Fail";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, BlogModel blog)
        {
            var item = await _db.blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found");
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            string message = await _db.SaveChangesAsync() > 0 ? "Successfully Update" : "Update Fail";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found");
            }
            _db.blogs.Remove(item);
            string message = await _db.SaveChangesAsync() > 0 ? "Successfully Delete" : "Delete Fail";
            return Ok(message);
        }
    }
}
