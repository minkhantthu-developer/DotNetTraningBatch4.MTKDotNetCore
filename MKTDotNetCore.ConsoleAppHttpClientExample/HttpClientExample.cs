using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MKTDotNetCore.ConsoleAppHttpClientExample
{
    public class HttpClientExample
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7185") };
        private readonly string _blogEndPoint = "api/blog";

        public async Task RunAsync()
        {
            await DeleteAsync(17);
        }

        public async Task ReadAsync()
        {
            var response = await _httpClient.GetAsync(_blogEndPoint);
            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return;
            }
            string jsonStr = await response.Content.ReadAsStringAsync();
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
            var response = await _httpClient.GetAsync($"{_blogEndPoint}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                string message=await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return;
            }
            string jsonStr = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr);
            Console.Write(JsonConvert.SerializeObject(item));
            Console.Write($"BlogId => {item.BlogId}");
            Console.Write($"BlogTitle => {item.BlogTitle}");
            Console.Write($"BlogAuthor => {item.BlogAuthor}");
            Console.Write($"BlogContent => {item.BlogContent}");
        }

        public async Task PostAsync(string title,string author,string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string blogJson=JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PostAsync(_blogEndPoint, httpContent);
            if(!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return;
            }
            string jsonStr=await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }

        public async Task PutAsync(int id,string title,string author,string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string blogJson = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PutAsync($"{_blogEndPoint}/{id}", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return;
            }
            string jsonStr = await response.Content.ReadAsStringAsync();
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
            string blogJson = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PatchAsync($"{_blogEndPoint}/{id}", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return;
            }
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_blogEndPoint}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return;
            }
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }
}
