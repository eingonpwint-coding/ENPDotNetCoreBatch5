using ENPDotNetCoreBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENPDotNetCoreBatch5.ConsoleApp
{
    public  class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=ENPDotNetBatch5;User ID=sa;Password=sasa@123;";

        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }

        public void Edit()
        {
            Console.Write("Blog Id: ");
            string id = Console.ReadLine()!;

            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                              FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";
            //if no use , paramas,
            //SqlParameterModel[] sqlParameters = new SqlParameterModel[1];
            //sqlParameters[0] = new SqlParameterModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //};
            //var dt = _adoDotNetService.Query(query, sqlParameters);

            //params mean it allows like an array,put all data like array 
            //but new parameter cannot add after params
            // Qurey(qurey, 1,2,3,etc)

            var dt = _adoDotNetService.Query(query, new SqlParameterModel("@BlogId", id));

            DataRow dr = dt.Rows[0];
            Console.WriteLine("Blog ID: " + dr["BlogId"]);
            Console.WriteLine("Title: " + dr["BlogTitle"]);
            Console.WriteLine("Author: " + dr["BlogAuthor"]);
            Console.WriteLine("Content: " + dr["BlogContent"]);
        }

        public void Create()
        {
            Console.WriteLine("Enter BlogTitle");
            string title = Console.ReadLine();

            Console.WriteLine("Enter BlogAuthor");
            string author = Console.ReadLine();

            Console.WriteLine("Enter BlogContent");
            string content = Console.ReadLine();

            string query = @"
                    INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent]
                               ,[DeleteFlag])
                         VALUES
                               (@BlogTitle
                               ,@BlogAuthor
                               ,@BlogContent
                               ,0)";

            //int result = _adoDotNetService.Execute(query, new SqlParameterModel
            //{
            //    Name = "@BlogTitle",
            //    Value = title
            //},
            //new SqlParameterModel
            //{
            //    Name = "@BlogAuthor",
            //    Value = author
            //},
            //new SqlParameterModel
            //{
            //    Name = "@BlogContent",
            //    Value = content 
            //});

           int result = _adoDotNetService.Execute(query,
           new SqlParameterModel("@BlogTitle", title),
           new SqlParameterModel("@BlogAuthor", author),
           new SqlParameterModel("@BlogContent", content));


            Console.WriteLine(result == 1 ? "Saving successful" : "Saving failed");
            Console.ReadKey();
        }

        public void Update()
        {
            Console.WriteLine("Enter Id :");
            string id = Console.ReadLine();

            Console.WriteLine("Enter BlogTitle");
            string title = Console.ReadLine();

            Console.WriteLine("Enter BlogAuthor");
            string author = Console.ReadLine();

            Console.WriteLine("Enter BlogContent");
            string content = Console.ReadLine();


            string query = @"
                    UPDATE [dbo].[Tbl_Blog]
                       SET [BlogTitle] = @BlogTitle
                          ,[BlogAuthor] = @BlogAuthor
                          ,[BlogContent] = @BlogContent
                          ,[DeleteFlag] = 0
                     WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query,
                   new SqlParameterModel("@BlogId", id),
                   new SqlParameterModel("@BlogTitle", title),
                   new SqlParameterModel("@BlogAuthor", author),
                   new SqlParameterModel("@BlogContent", content));

            Console.WriteLine(result == 1 ? "Updating successful" : "Updating failed");
            Console.ReadKey();

        }

        public void Delete()
        {
            Console.WriteLine("Enter Id :");
            string id = Console.ReadLine();

           
            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [DeleteFlag] = 1
                         WHERE BlogId = @BlogId;";


            int result = _adoDotNetService.Execute(query, new SqlParameterModel("@BlogId", id));

            Console.WriteLine(result == 1 ? "Deleting successful" : "Deleting failed");
            Console.ReadKey();

        }
    }
}
