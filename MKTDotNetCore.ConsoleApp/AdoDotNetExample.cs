
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {

        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "LAPTOP-TTIU8JF8",
            InitialCatalog = "TestDB",
            UserID = "sa",
            Password = "Minkhantthu3367",
            TrustServerCertificate =true
        };

        public void Read()
        {
            SqlConnection connection=new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Blog]";
            SqlCommand cmd=new SqlCommand(query, connection);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            connection.Close();
            Console.WriteLine("Connection Close");
            foreach(DataRow dr in dt.Rows)
            {
                Console.WriteLine("BlogID => " + dr["BlogId"]);
                Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
                Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
                Console.WriteLine("BlogContent => " + dr["BlogContent"]);
                Console.WriteLine("---------------------");
            }
        }

        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Blog] Where [BlogId]=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            connection.Close();
            if (dt.Rows.Count <= 0)
            {
                Console.WriteLine("No data found");
                return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine("Connection Close");
            Console.WriteLine("BlogID => " + dr["BlogId"]);
            Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
            Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
            Console.WriteLine("BlogContent => " + dr["BlogContent"]);
        }

        public void Create(string BlogTitle,string BlogAuthor,string BlogContent)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            string query = @"INSERT INTO [dbo].[Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Connection Close");
            string message = result > 0 ? "Successfully Save" : "Fail to Save";
            Console.WriteLine(message);
        }

        public void Update(int id,string BlogTitle,string BlogAuthor,string BlogContent)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            string query = @"UPDATE [dbo].[Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId]=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", BlogContent);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result=cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Connection Close");
            string message = result > 0 ? "Succcessfully Update" : "Update Fail";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            string query = @"DELETE FROM [dbo].[Blog]
      WHERE [BlogId]=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Connection Close");
            string message = result > 0 ? "Succcessfully Delete" : "Delete Fail";
            Console.WriteLine(message);
        }

    }
}
