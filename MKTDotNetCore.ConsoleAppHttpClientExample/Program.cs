// See https://aka.ms/new-console-template for more information
using MKTDotNetCore.ConsoleAppHttpClientExample;

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadKey();
