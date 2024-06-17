using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.ConsoleApp
{
    public class EfCoreExample
    { 

        public void Run()
        {
            Delete(9);
            Read();
        }

        private void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.blogs.ToList();
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
            AppDbContext db = new AppDbContext();
            var item = db.blogs.FirstOrDefault(x=>x.BlogId==id);    
            if(item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            Console.WriteLine("BlogId => " + item.BlogId);
            Console.WriteLine("BlogTitle => " + item.BlogTitle);
            Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
            Console.WriteLine("BlogContent => " + item.BlogContent);
        }

        private void Create(string title,string author,string content)
        {
            AppDbContext db = new AppDbContext();
            BlogDto blog = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            db.blogs.Add(blog);
            int result = db.SaveChanges();
            string message = result > 0 ? "Successfully Save" : "Fail to save";
            Console.WriteLine(message);
        }

        private void Update(int id,string title,string author,string content)
        {
            AppDbContext db = new AppDbContext();
            var item =  db.blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            item.BlogTitle = title;
            item.BlogAuthor= author;
            item.BlogContent = content;
            int result = db.SaveChanges();
            string message = result > 0 ? "Successfully Update" : "Fail to Update";
            Console.WriteLine(message);
        }

        private  void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            db.blogs.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Successfully Delete" : "Fail to Delete";
            Console.WriteLine(message);
        }

    }
}
