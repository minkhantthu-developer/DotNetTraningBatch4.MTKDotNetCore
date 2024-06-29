using Microsoft.EntityFrameworkCore;
using MKTDotNetCore.MinimalApi.Db;
using MKTDotNetCore.MinimalApi.Models;

namespace MKTDotNetCore.MinimalApi.Features.Blog
{
    public static class BlogService
    {
        public static void AddBlogFeature(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/Blog", async (AppDbContext db) =>
            {
                var lst = await db.blogs.AsNoTracking().ToListAsync();
                return Results.Ok(lst);
            }).WithName("GetBlog")
            .WithOpenApi();

            app.MapGet("api/Blog/{id}", async (AppDbContext db, int id) =>
            {
                var item = await db.blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No data found");
                }
                return Results.Ok(item);
            }).WithName("GetBlogById").WithOpenApi();

            app.MapPost("api/Blog", async (AppDbContext db, BlogModel blog) =>
            {
                await db.blogs.AddAsync(blog);
                int result = await db.SaveChangesAsync();
                string message = result > 0 ? "Successfully Save" : "Saving Fail";
                return Results.Ok(message);
            }).WithName("PostBlog").WithOpenApi();

            app.MapPut("api/Blog/{id}", async (AppDbContext db, int id, BlogModel blog) =>
            {
                var item = await db.blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null) return Results.NotFound("No data found");
                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
                db.Entry(item).State = EntityState.Modified;
                int result = await db.SaveChangesAsync();
                string message = result > 0 ? "Successfully Update" : "Updating Fail";
                return Results.Ok(message);
            }).WithName("PutBlog")
            .WithOpenApi();

            app.MapPatch("api/Blog/{id}", async (AppDbContext db, int id, BlogModel blog) =>
            {
                var item = await db.blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null) return Results.NotFound("No data found");
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
                db.Entry(item).State = EntityState.Modified;
                int result = await db.SaveChangesAsync();
                string message = result > 0 ? "Successfully Update" : "Updating Fail";
                return Results.Ok(message);
            }).WithName("Patch")
                .WithOpenApi();

            app.MapDelete("api/Blog/{id}", async (AppDbContext db, int id) =>
            {
                var item = await db.blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null) return Results.NotFound("No data found");
                db.blogs.Remove(item);
                int result = await db.SaveChangesAsync();
                string message = result > 0 ? "Successfully Delete" : "Deleting Fail";
                return Results.Ok(message);
            }).WithName("DeleteBlog")
            .WithOpenApi();
        }
    }
}
