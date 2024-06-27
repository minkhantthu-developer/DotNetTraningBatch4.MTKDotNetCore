using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKTDotNetCore.ConsoleApp.Dtos;

namespace MKTDotNetCore.ConsoleApp.EfCoreExamples
{
    public class EfCoreExample
    {
        private readonly AppDbContext _context;

        public EfCoreExample(AppDbContext context)
        {
            _context = context;
        }

        public void Run()
        {
            Read();
        }

        private void Read()
        {
           
            var lst =_context.blogs.ToList();
            foreach (var item in lst)
            {
                Console.WriteLine("BlogId => " + item.BlogId);
                Console.WriteLine("BlogTitle => " + item.BlogTitle);
                Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
                Console.WriteLine("BlogContent => " + item.BlogContent);
                Console.WriteLine("------------");
            }
        }

        private void Edit(int id)
        {
           
            var item =_context.blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            Console.WriteLine("BlogId => " + item.BlogId);
            Console.WriteLine("BlogTitle => " + item.BlogTitle);
            Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
            Console.WriteLine("BlogContent => " + item.BlogContent);
        }

        private void Create(string title, string author, string content)
        {
           
            BlogDto blog = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
           _context.blogs.Add(blog);
            int result =_context.SaveChanges();
            string message = result > 0 ? "Successfully Save" : "Fail to save";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
           
            var item =_context.blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            int result =_context.SaveChanges();
            string message = result > 0 ? "Successfully Update" : "Fail to Update";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
           
            var item =_context.blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
           _context.blogs.Remove(item);
            int result =_context.SaveChanges();
            string message = result > 0 ? "Successfully Delete" : "Fail to Delete";
            Console.WriteLine(message);
        }

    }
}
