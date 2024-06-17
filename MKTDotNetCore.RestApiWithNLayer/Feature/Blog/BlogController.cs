using Microsoft.AspNetCore.Http;


namespace MKTDotNetCore.RestApiWithNLayer.Feature.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _bl_blog;
        
        public BlogController()
        {
            _bl_blog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst=_bl_blog.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item=_bl_blog.GetBlog(id);
            if(item is null)
            {
                return NotFound("No data found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult PostBlog(BlogModel requestModel)
        {
            int result = _bl_blog.CreateBlog(requestModel);
            string message = result > 0 ? "Successfully Save" : "Fail to save";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult PutBlog(int id,BlogModel requestModel)
        {
            int result = _bl_blog.UpdateBlog(id, requestModel);
            string message = result > 0 ? "Successfully Update" : "Fail to Update";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            int result = _bl_blog.DeleteBlog(id);
            string message = result > 0 ? "Successfully Delete" : "Fail to Delete";
            return Ok(message);
        }
    }
}
