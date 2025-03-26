using ENPDotNetCoreBatch5.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadLine();
//Console.ReadKey();
//md

//string connectionString = "Data Source=.;Initial Catalog=ENPDotNetBatch5;User ID=sa;Password=sasa@123;";

//Console.WriteLine("connectionString"+ connectionString);

//SqlConnection connection = new SqlConnection(connectionString);

//Console.WriteLine("Connection opening ......");
//connection.Open();
//Console.WriteLine("Connection opened ......");

//string query = @"SELECT [BlogId]
//      ,[BlogTitle]
//      ,[BlogAuthor]
//      ,[BlogContent]
//      ,[DeleteFlag]
//  FROM [dbo].[Tbl_Blog] where [DeleteFlag] = 0";

////if use DataAdapter ,....
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//adapter.Fill(dt); // fill = execute (in database)

//Console.WriteLine("Connection closing ......");

//connection.Close();
//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogId"]);
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthor"]);
//    Console.WriteLine(dr["BlogContent"]);
//    //Console.WriteLine(dr["DeleteFlag"]);
//}

//Console.WriteLine("Connection closed ......");
//Console.ReadKey();

//if use DataReader , 
//SqlCommand cmd1 = new SqlCommand(query, connection);
//SqlDataReader reader = cmd1.ExecuteReader();

//while(reader.Read())
//{
//    Console.WriteLine(reader["BlogId"]);
//    Console.WriteLine(reader["BlogTitle"]);
//    Console.WriteLine(reader["BlogAuthor"]);
//    Console.WriteLine(reader["BlogContent"]);
//    //Console.WriteLine(dr["DeleteFlag"]);
//}
//Console.WriteLine("Connection closing ......");
//connection.Close();


//Console.WriteLine("Connection closed ......");
//Console.ReadKey();

//Create

//Console.WriteLine("Enter BlogTitle");
//string title = Console.ReadLine();

//Console.WriteLine("Enter BlogAuthor");
//string author = Console.ReadLine();

//Console.WriteLine("Enter BlogContent");
//string content = Console.ReadLine();


//string connectionString2 = "Data Source= .; Initial Catalog=ENPDotNetBatch5 ;User ID = sa; Password = sasa@123 ";
//SqlConnection connection2 = new SqlConnection(connectionString2);
//connection2.Open();
////if can use $@"INSERT INTO [dbo].[Tbl_Blog]([BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag]) VALUES ('{BlogTitle}', '{BlogAuthor}', '{BlogContent}', 0)";
////To avoid sql Injection, use @ is better.
//string query2 = @"
//INSERT INTO [dbo].[Tbl_Blog]
//           ([BlogTitle]
//           ,[BlogAuthor]
//           ,[BlogContent]
//           ,[DeleteFlag])
//     VALUES
//           (@BlogTitle
//           ,@BlogAuthor
//           ,@BlogContent
//           ,0)";
//SqlCommand cmd2 = new SqlCommand(query2, connection2);

//cmd2.Parameters.AddWithValue("@BlogTitle", title);
//cmd2.Parameters.AddWithValue ("@BlogAuthor", author);
//cmd2.Parameters.AddWithValue("@BlogContent", content);

////SqlDataAdapter adapter1 = new SqlDataAdapter(cmd2);
////DataTable tbl2 = new DataTable();
////adapter1.Fill(tbl2);

//int result = cmd2.ExecuteNonQuery();

//connection2.Close();

////if (result == 1)
////{
////    Console.WriteLine("Saving successful");
////}
////else
////{
////    Console.WriteLine("Saving Failed");
////}

//Console.WriteLine(result == 1 ? "Saving successful" : "Saving failed");


//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();

DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("testing", "testing", "testing");
//dapperExample.Edit(1);
//dapperExample.Update(1, "change", "change", "change");
//dapperExample.Delete(1);

EFCoreExample test = new EFCoreExample();
test.Read();
Console.ReadKey();