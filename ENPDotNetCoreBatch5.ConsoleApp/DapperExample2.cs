using Dapper;
using ENPDotNetCoreBatch5.ConsoleApp.Models;
using ENPDotNetCoreBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENPDotNetCoreBatch5.ConsoleApp
{
    internal class DapperExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=ENPDotNetBatch5;User ID=sa;Password=sasa@123;";

        private readonly DapperService _dapperService;

        public DapperExample2(string connectionString)
        {
            _dapperService = new DapperService(connectionString);
        }
        public void Read()
        {
            //Console.WriteLine("Query is: " + query);
            string query = "select * from tbl_Blog where DeleteFlag=0";
            var lst = _dapperService.Query<BlogDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
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

            var result = _dapperService.Execute(query, new BlogDataModel
            {
                 BlogTitle = title,
                 BlogAuthor = author,
                 BlogContent = content
            });

            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
            
        }


        public void Edit(int id)
        {
             string query = "select * from tbl_Blog where DeleteFlag=0 and BlogId = @BlogId;";
             var item = _dapperService.QueryFirstOrDefault<BlogDataModel>(query, new BlogDataModel
             {
                    BlogId = id
             });

            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

        }

        public void Update(int id, string title, string author, string content)
        {
            string query = @" UPDATE [dbo].[Tbl_Blog]
                       SET [BlogTitle] = @BlogTitle
                          ,[BlogAuthor] = @BlogAuthor
                          ,[BlogContent] = @BlogContent
                          ,[DeleteFlag] = 0
                     WHERE BlogId = @BlogId;";

            var result = _dapperService.Execute(query, new BlogDataModel
            {
                 BlogId = id,
                 BlogTitle = title,
                 BlogAuthor = author,
                 BlogContent = content
            });

            Console.WriteLine(result == 1 ? "Updating Successful" : "Updating Failed");
            
        }

        public void Delete(int id)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [DeleteFlag] = 1
                         WHERE BlogId = @BlogId;";

            var result = _dapperService.Execute(query, new BlogDataModel
            {
               BlogId = id,

            });

            Console.WriteLine(result == 1 ? "Deleting Successful" : "Deleting Failed");
            
        }
    }
}
