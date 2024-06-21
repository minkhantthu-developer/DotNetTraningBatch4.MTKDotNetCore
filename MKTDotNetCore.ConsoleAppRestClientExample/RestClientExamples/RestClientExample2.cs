
using MKTDotNetCore.ConsoleAppRestClientExample.Models;
using MKTDotNetCore.Shared.HttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.ConsoleAppRestClientExample.RestClientExamples
{
    public class RestClientExample2
    {
        private readonly RestClientService _restService;
        private readonly string blogEndPoint = "api/blog";

        public RestClientExample2()
        {
            _restService = new RestClientService("https://localhost:7185");
        }

        public async Task RunAsync()
        {
            await DeleteAsync(27);
        }

        public async Task ReadAsync()
        {
            var lst = await _restService.ExecuteAsync<List<BlogModel>>(blogEndPoint, EnumHttpMethod.Get);
            foreach(var item in lst)
            {
                Console.WriteLine($"BlogId=> {item.BlogId}");
                Console.WriteLine($"BlogTitle=> {item.BlogTitle}");
                Console.WriteLine($"BlogAuthor=> {item.BlogAuthor}");
                Console.WriteLine($"BlogContent=> {item.BlogContent}");
                Console.WriteLine("------------------------");
            }
        }

        public async Task EditAsync(int id)
        {
            var item = await _restService.ExecuteAsync<BlogModel>($"{blogEndPoint}/{id}", EnumHttpMethod.Get);
            if(item is null || item.BlogId==0)
            {
                Console.WriteLine("No data found");
                return;
            }
            Console.WriteLine($"BlogId=> {item.BlogId}");
            Console.WriteLine($"BlogTitle=> {item.BlogTitle}");
            Console.WriteLine($"BlogAuthor=> {item.BlogAuthor}");
            Console.WriteLine($"BlogContent=> {item.BlogContent}");
        }

        public async Task CreateAsync(string title,string author,string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var response=await _restService.ExecuteAsync<ResponseModel>(blogEndPoint, EnumHttpMethod.Post,blog);
            if (response.IsError)
            {
                Console.WriteLine(response.message);return;
            }
            Console.WriteLine(response.IsSuccess);
            Console.WriteLine(response.message);
        }

        public async Task PutAsync(int id,string title,string author,string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var response=await _restService.ExecuteAsync<ResponseModel>($"{blogEndPoint}/{id}", EnumHttpMethod.Put,blog);
            if (response.IsError)
            {
                Console.WriteLine(response.message); return;
            }
            Console.WriteLine(response.IsSuccess);
            Console.WriteLine(response.message);
        }

        public async Task PatchAsync(int id, string title, string author, string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var response = await _restService.ExecuteAsync<ResponseModel>($"{blogEndPoint}/{id}", EnumHttpMethod.Patch, blog);
            if (response.IsError)
            {
                Console.WriteLine(response.message); return;
            }
            Console.WriteLine(response.IsSuccess);
            Console.WriteLine(response.message);
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _restService.ExecuteAsync<ResponseModel>($"{blogEndPoint}/{id}", EnumHttpMethod.Delete);
            if (response.IsError)
            {
                Console.WriteLine(response.message); return;
            }
            Console.WriteLine(response.IsSuccess);
            Console.WriteLine(response.message);
        }
    }
}
