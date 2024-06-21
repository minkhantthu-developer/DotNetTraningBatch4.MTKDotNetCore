using MKTDotNetCore.ConsoleAppRestClientExample.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MKTDotNetCore.ConsoleAppRestClientExample.RestClientExamples
{
    public class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7185"));
        private readonly string _blogEndPoint = "api/blog";

        public async Task RunAsync()
        {
            await DeleteAsync(18);
        }

        public async Task ReadAsync()
        {
            RestRequest request = new RestRequest(_blogEndPoint, Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
                return;
            }
            string jsonStr = response.Content!;
            var lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr);
            foreach (var item in lst!)
            {
                Console.Write(JsonConvert.SerializeObject(item));
                Console.Write($"BlogId => {item.BlogId}");
                Console.Write($"BlogTitle => {item.BlogTitle}");
                Console.Write($"BlogAuthor => {item.BlogAuthor}");
                Console.Write($"BlogContent => {item.BlogContent}");
            }
        }

        public async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (!response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
                return;
            }
            string jsonStr = response.Content!;
            var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr);
            Console.Write(JsonConvert.SerializeObject(item));
            Console.Write($"BlogId => {item.BlogId}");
            Console.Write($"BlogTitle => {item.BlogTitle}");
            Console.Write($"BlogAuthor => {item.BlogAuthor}");
            Console.Write($"BlogContent => {item.BlogContent}");
        }

        public async Task PostAsync(string title, string author, string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}", Method.Post);
            restRequest.AddJsonBody(blog);
            var response = await _client.ExecuteAsync(restRequest);
            if (!response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
                return;
            }
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }

        public async Task PutAsync(int id, string title, string author, string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Put);
            restRequest.AddJsonBody(blog);
            var response = await _client.ExecuteAsync(restRequest);
            if (!response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
                return;
            }
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }

        public async Task PatchAsync(int id, string? title, string? author, string? content)
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
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Patch);
            restRequest.AddJsonBody(blog);
            var response = await _client.ExecuteAsync(restRequest);
            if (!response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
                return;
            }
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }

        public async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(restRequest);
            if (!response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
                return;
            }
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }

    }
}
