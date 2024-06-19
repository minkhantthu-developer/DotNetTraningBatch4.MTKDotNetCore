using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.ConsoleAppRefitExample
{
    public class RefitExample
    {
        IBlogApi service = RestService.For<IBlogApi>("https://localhost:7185");

        public async Task RunAsync()
        {
            await CreateAsync("KayKhine","K3","KKube");
        }

        public async Task ReadAsync()
        {
            var lst = await service.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine($"BlogId => {item.BlogId}");
                Console.WriteLine($"BlogTitle => {item.BlogTitle}");
                Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
                Console.WriteLine($"BlogContent => {item.BlogContent}");
                Console.WriteLine("-------------------------");
            }
        }

        public async Task EditAsync(int id)
        {
            try
            {
                var item = await service.GetBlog(id);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode);
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task CreateAsync(string title, string author, string content)
        {
            try
            {
                BlogModel blog = new BlogModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                };
                string message = await service.PostBlog(blog);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode);
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task PutBlog(int id, string title, string author, string content)
        {
            try
            {
                BlogModel blog = new BlogModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                };
                string message = await service.PutBlog(id, blog);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode);
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task PatchBlog(int id, string title, string author, string content)
        {
            try
            {
                BlogModel blog = new BlogModel();
                if (!string.IsNullOrEmpty(title))
                {
                    blog.BlogTitle = title;
                }
                if (!string.IsNullOrEmpty(author))
                {
                    blog.BlogAuthor = author;
                }
                if (!string.IsNullOrEmpty(content))
                {
                    blog.BlogContent = content;
                }
                string message = await service.PatchBlog(id, blog);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode);
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task DeleteBlog(int id)
        {
            try
            {
                string message = await service.DeleteBlog(id);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode);
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
