
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query,params SqlParameter[]? param)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd=new SqlCommand(query,connection);
            cmd.Parameters.AddRange(param);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            connection.Close();
            string jsonStr = JsonConvert.SerializeObject(dt);
            var lst=JsonConvert.DeserializeObject<List<T>>(jsonStr);
            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, params SqlParameter[]? param)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(param);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            connection.Close();
            string jsonStr= JsonConvert.SerializeObject(dt);
            var lst=JsonConvert.DeserializeObject<List<T>>(jsonStr);
            if (lst!.Count == 0)
            {
                return default(T);
            }
            return lst[0];
        }

        public int Execute(string query,params SqlParameter[]? param)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(param);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
