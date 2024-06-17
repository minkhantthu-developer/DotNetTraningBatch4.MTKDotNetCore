
using Dapper;
using Microsoft.Data.SqlClient;
using MKTDotNetCore.ConsoleApp.Dtos;
using MKTDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.ConsoleApp.DapperExamples;

public class DapperExample
{
    public void Run()
    {
        Delete(7);
        Read();
    }

    private void Read()
    {
        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Blog]";
        List<BlogDto> lst = db.Query<BlogDto>(query).ToList();
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
        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Blog] Where [BlogId]=@BlogId";
        var item = db.Query<BlogDto>(query, new BlogDto { BlogId = id }).FirstOrDefault();
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
        string query = @"INSERT INTO [dbo].[Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        int result = db.Execute(query, blog);
        string message = result > 0 ? "Successfully Save" : "Fail to save";
        Console.WriteLine(message);
    }

    private void Update(int id, string title, string author, string content)
    {
        BlogDto blog = new BlogDto
        {
            BlogId = id,
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        string query = @"UPDATE [dbo].[Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId]=@BlogId";
        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        int result = db.Execute(query, blog);
        string message = result > 0 ? "Successfully Update" : "Fail to Update";
        Console.WriteLine(message);
    }

    private void Delete(int id)
    {
        string query = @"DELETE FROM [dbo].[Blog]
      WHERE [BlogId]=@BlogId";
        using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        int result = db.Execute(query, new BlogDto { BlogId = id });
        string message = result > 0 ? "Successfully Delete" : "Fail to Delete";
        Console.WriteLine(message);
    }
}
