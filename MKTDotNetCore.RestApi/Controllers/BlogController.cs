
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
                return NotFound(new ResponseModel { IsSuccess = false, message = "No data found" });
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BlogModel blog)
        {
            _db.blogs.Add(blog);
            int result = await _db.SaveChangesAsync();
            var model = result > 0 ?
                new ResponseModel
                {
                    IsSuccess = true,
                    message = "Successfully Save"
                } : new ResponseModel
                {
                    IsSuccess = false,
                    message = "fail to Create"
                };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BlogModel blog)
        {
            var item = await _db.blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound(new ResponseModel { IsSuccess = false, message = "No data found" });
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            int result = await _db.SaveChangesAsync();
            var model = result > 0 ? new ResponseModel { IsSuccess = true, message = "Successfully Update" } :
                                    new ResponseModel { IsSuccess = false, message = "Successfully Update" };
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, BlogModel blog)
        {
            var item = await _db.blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound(new ResponseModel { IsSuccess = false, message = "No data found" });
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
            int result = await _db.SaveChangesAsync();
            var model = result > 0 ? new ResponseModel { IsSuccess = true, message = "Successfully Update" } :
                                     new ResponseModel { IsSuccess = false, message = "Successfully Update" };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound(new ResponseModel { IsSuccess = false, message = "No data found" });
            }
            _db.blogs.Remove(item);
            int result = await _db.SaveChangesAsync();
            var model = result > 0 ? new ResponseModel { IsSuccess = true, message = "Successfully Delete" } :
                                     new ResponseModel { IsSuccess = false, message = "Fail to Delete" };
            return Ok(model);
        }
    }
}
