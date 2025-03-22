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
        private readonly string _connectionString = "Data Source=.;Initial Catalog=ENPDotNetBatch5;User ID=sa;Password=sasa@123;";
        public void Read()
        {
            
            Console.WriteLine("connectionString" + _connectionString);

            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("Connection opening ......");
            connection.Open();
            Console.WriteLine("Connection opened ......");

            string query = @"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag]
              FROM [dbo].[Tbl_Blog] where [DeleteFlag] = 0";

            //if use DataAdapter ,....
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt); // fill = execute (in database)

            Console.WriteLine("Connection closing ......");

            connection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
                //Console.WriteLine(dr["DeleteFlag"]);
            }

            Console.WriteLine("Connection closed ......");
            //Console.ReadKey();
        }

    }
}
