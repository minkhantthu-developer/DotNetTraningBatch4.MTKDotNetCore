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
    public class BlogAdo2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoService;
        
        public BlogAdo2Controller(AdoDotNetService adoService)
        {
            _adoService = adoService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Blog]";
           var lst = _adoService.Query<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found ");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new("@BlogTitle",blog.BlogTitle),
                new("@BlogAuthor",blog.BlogAuthor),
                new("@BlogContent",blog.BlogContent),
            };
            int result = _adoService.Execute(query, parms.ToArray());
            string message = result > 0 ? "Successfully Save" : "Saving Fail";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult PutBlog(int id,BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found ");
            }
            string query = @"UPDATE [dbo].[Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId]=@BlogId";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new("@BlogTitle",blog.BlogTitle),
                new("@BlogAuthor",blog.BlogAuthor),
                new("@BlogContent",blog.BlogContent),
                new("@BlogId",item.BlogId)
            };
            int result=_adoService.Execute(query,parms.ToArray());
            string message = result > 0 ? "Successfully Update" : "Updating Fail";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id,BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found ");
            }
            List<SqlParameter> parms = new List<SqlParameter>();
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
                parms.Add(new("@BlogTitle", blog.BlogTitle)); 
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
                parms.Add(new("@BlogAuthor", blog.BlogAuthor));
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += " [BlogContent] = @BlogContent, ";
                parms.Add(new("@BlogContent", blog.BlogContent));
            }
            if (condition.Length == 0)
            {
                return BadRequest("No data to update");
            }
            parms.Add(new("@BlogId", item.BlogId));
            condition = condition.Substring(0, condition.Length - 2);
            string query = $@"UPDATE [dbo].[Blog]
   SET {condition}
 WHERE [BlogId]=@BlogId";
            int result = _adoService.Execute(query, parms.ToArray());
            string message = result > 0 ? "Successfully Update" : "Updating Fail";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Blog]
      WHERE [BlogId]=@BlogId";
            int result=_adoService.Execute(query,new SqlParameter("@BlogId",id));
            string message = result > 0 ? "Successfully Delete" : "Deleting Fail";
            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Blog] Where [BlogId]=@BlogId";
            var item = _adoService.QueryFirstOrDefault<BlogModel>(query, new SqlParameter("@BlogId", id));
            return item;
        }
    }
}
