// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MKTDotNetCore.ConsoleApp.AdoDotNetExamples;
using MKTDotNetCore.ConsoleApp.DapperExamples;
using MKTDotNetCore.ConsoleApp.EfCoreExamples;
using MKTDotNetCore.ConsoleApp.Services;

//AdoDotNetExample ado = new AdoDotNetExample();
//ado.Delete(6);
/*ado.Read()*/
//ado.Update(1, "Taw Thar ", "Zaya Min Htet", "Thone Gwa Thar");

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();
string connectionString = ConnectionStrings.SqlConnectionStringBuilder.ConnectionString;
SqlConnectionStringBuilder sqlConnectionString = ConnectionStrings.SqlConnectionStringBuilder;
var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotNetExample(sqlConnectionString))
    .AddScoped(n => new DapperExample(sqlConnectionString))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped<EfCoreExample>()
    .BuildServiceProvider();
var dapperExample=serviceProvider.GetRequiredService<DapperExample>();
dapperExample.Run();