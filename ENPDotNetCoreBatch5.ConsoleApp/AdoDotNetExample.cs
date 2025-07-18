 using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection.Metadata;

namespace ENPDotNetCoreBatch5.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = AppSettings.ConnectionString;

        public void Read()
        {
            Console.WriteLine("Connection string: " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("Connection opening...");
            connection.Open();
            Console.WriteLine("Connection opened.");

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                //Console.WriteLine(dr["DeleteFlag"]);
            }

            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //    //Console.WriteLine(dr["DeleteFlag"]);
            //}

            Console.WriteLine("Connection closing...");
            connection.Close();
            Console.WriteLine("Connection closed.");

            // DataSet
            // DataTable
            // DataRow
            // DataColumn

            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //    //Console.WriteLine(dr["DeleteFlag"]);
            //}
        }

        public void Create()
        {
            Console.WriteLine("Enter BlogTitle");
            string title = Console.ReadLine();

            Console.WriteLine("Enter BlogAuthor");
            string author = Console.ReadLine();

            Console.WriteLine("Enter BlogContent");
            string content = Console.ReadLine();



            SqlConnection connection2 = new SqlConnection(_connectionString);
            connection2.Open();
            //if can use $@"INSERT INTO [dbo].[Tbl_Blog]([BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag]) VALUES ('{BlogTitle}', '{BlogAuthor}', '{BlogContent}', 0)";
            //To avoid sql Injection, use @ is better.
            string query2 = @"
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
            SqlCommand cmd2 = new SqlCommand(query2, connection2);

            cmd2.Parameters.AddWithValue("@BlogTitle", title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", author);
            cmd2.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd2.ExecuteNonQuery();

            connection2.Close();

            Console.WriteLine(result == 1 ? "Saving successful" : "Saving failed");
            Console.ReadKey();
        }

        public void Edit()
        {
            Console.WriteLine("Enter Id :");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
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

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
                    UPDATE [dbo].[Tbl_Blog]
                       SET [BlogTitle] = @BlogTitle
                          ,[BlogAuthor] = @BlogAuthor
                          ,[BlogContent] = @BlogContent
                          ,[DeleteFlag] = 0
                     WHERE BlogId = @BlogId";

            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd2.Parameters.AddWithValue("@BlogId", id);
            cmd2.Parameters.AddWithValue("@BlogTitle", title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", author);
            cmd2.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Updating successful" : "Updating failed");
            Console.ReadKey();

        }

        public void Delete()
        {
            Console.WriteLine("Enter Id :");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [DeleteFlag] = 1
                         WHERE BlogId = @BlogId;";

            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd2.Parameters.AddWithValue("@BlogId", id);

            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Deleting successful" : "Deleting failed");
            Console.ReadKey();

        }
    }
}
