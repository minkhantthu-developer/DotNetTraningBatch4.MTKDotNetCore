// See https://aka.ms/new-console-template for more information
using MKTDotNetCore.RestApiWithNLayer.Feature.Blog;

Console.WriteLine("Hello, World!");
BL_Blog bl_blog = new BL_Blog();
var lst =bl_blog.GetBlogs();
foreach (var item in lst)
{
    Console.WriteLine($"BlogID => {item.BlogId}");
    Console.WriteLine($"BlogTitle => {item.BlogTitle}");
    Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
    Console.WriteLine($"BlogContent => {item.BlogContent}");
    Console.WriteLine("--------------");
}
