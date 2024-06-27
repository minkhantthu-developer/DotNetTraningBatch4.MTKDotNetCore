using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MKTDotNetCore.RestApi.Models;
using MKTDotNetCore.Shared;
using System.Data;

namespace MKTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperService _dapperService;

        public BlogDapper2Controller(DapperService dapperService)
        {
            _dapperService = dapperService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Blog]";
            var lst = _dapperService.Query<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult PostBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Successfully Save" : "Saving Fail";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult PutBlog(int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId]=@BlogId";
            var item = FindById(id);
            if (item is null)
                return NotFound("No data found");
            blog.BlogId = item.BlogId;
            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Successfully Update" : "Update Fail";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
                return NotFound("No data found");
            blog.BlogId = item.BlogId;
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += " [BlogContent] = @BlogContent, ";
            }
            if (condition.Length == 0)
            {
                return BadRequest("No data to update");
            }
            condition = condition.Substring(0, condition.Length - 2);
            string query = $@"UPDATE [dbo].[Blog]
   SET {condition}
 WHERE [BlogId]=@BlogId";
            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Successfully Update" : "Update Fail";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Deleteblog(int id)
        {
            string query = @"DELETE FROM [dbo].[Blog]
      WHERE [BlogId]=@BlogId";
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found");
            }
            int result = _dapperService.Execute(query, new BlogModel { BlogId = item.BlogId });
            string message = result > 0 ? "Successfully Delete" : "Delete Fail";
            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Blog] Where [BlogId]=@BlogId";
           var item=_dapperService.QueryFirstOrDefault<BlogModel>(query,new BlogModel { BlogId=id});
            return item;
        }
    }
}
