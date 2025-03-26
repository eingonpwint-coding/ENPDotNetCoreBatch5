using Dapper;
using ENPDotNetCoreBatch5.ConsoleApp.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;


namespace ENPDotNetCoreBatch5.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=ENPDotNetBatch5;User ID=sa;Password=sasa@123;";

        public void Read()
        {
            //using(IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    string query = "select * from dbl_Blog where DeleteFlag=0";
            //    var lst = db.Query(query).ToList();

            //    foreach (var item in lst)
            //    {
            //        Console.WriteLine(item.BlogId);
            //        Console.WriteLine(item.BlogTitle);
            //        Console.WriteLine(item.BlogAuthor);
            //        Console.WriteLine(item.BlogContent);
            //    }
            //}

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_Blog where DeleteFlag=0";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }
        }

        
    }
}
