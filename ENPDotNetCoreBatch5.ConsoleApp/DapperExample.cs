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

        public void Create(string title, string author, string content)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent]
                               ,[DeleteFlag])
                         VALUES
                               (@BlogTitle
                               ,@BlogAuthor
                               ,@BlogContent
                               ,0)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
            }
        }

        public void Edit(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_Blog where DeleteFlag=0 and BlogId = @BlogId;";
                var item = db.Query<BlogDataModel>(query, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();

                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

            }
        }

        public void Update(int id, string title, string author, string content)
        {
            string query = @" UPDATE [dbo].[Tbl_Blog]
                       SET [BlogTitle] = @BlogTitle
                          ,[BlogAuthor] = @BlogAuthor
                          ,[BlogContent] = @BlogContent
                          ,[DeleteFlag] = 0
                     WHERE BlogId = @BlogId;";


            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var result = db.Execute(query, new BlogDataModel
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                Console.WriteLine(result == 1 ? "Updating Successful" : "Updating Failed");
            }
        }

        public void Delete(int id)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [DeleteFlag] = 1
                         WHERE BlogId = @BlogId;";


            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var result = db.Execute(query, new BlogDataModel
                {
                    BlogId = id,

                });

                Console.WriteLine(result == 1 ? "Deleting Successful" : "Deleting Failed");
            }
        }

    }
}
