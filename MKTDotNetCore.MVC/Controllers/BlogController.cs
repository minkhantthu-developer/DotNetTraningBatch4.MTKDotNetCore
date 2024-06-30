using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKTDotNetCore.MVC.Db;
using MKTDotNetCore.MVC.Models;

namespace MKTDotNetCore.MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _context.blogs
                .OrderByDescending(x => x.BlogId)
                .AsNoTracking()
                .ToListAsync();
            return View(lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogCreate(BlogModel requestModel)
        {
            await _context.blogs.AddAsync(requestModel);
            int result = await _context.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var item = await _context.blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if(item is null)
            {
                return Redirect("/Blog");
            }
            return View("BlogEdit",item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id,BlogModel requestModel)
        {
            var item = await _context.blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Redirect("/Blog");
            }
            item.BlogTitle = requestModel.BlogTitle;
            item.BlogAuthor=requestModel.BlogAuthor;
            item.BlogContent = requestModel.BlogContent;
            _context.Entry(item).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            var item = await _context.blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Redirect("/Blog");
            }
            _context.blogs.Remove(item);
            int result = await _context.SaveChangesAsync();
            return Redirect("/Blog");
        }
    }
}
