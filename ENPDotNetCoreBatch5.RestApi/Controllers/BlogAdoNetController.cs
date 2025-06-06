using ENPDotNetCoreBatch5.RestApi.DataModels;
using ENPDotNetCoreBatch5.RestApi.VIewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ENPDotNetCoreBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=ENPDotNetBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> blogs = new List<BlogViewModel>();
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);

                var item = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),

                };
                blogs.Add(item);
            }
            conn.Close();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            BlogViewModel blog = new BlogViewModel();
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                blog = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),

                };
            }
            conn.Close();
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
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

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = cmd.ExecuteNonQuery();
            con.Close();
            string message = result > 0 ? "Saving successful" : "Saving failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            string checkQuery = @"select count(*) from [ENPDotNetBatch5].[dbo].[Tbl_Blog] where BlogId = @BlogId";
            string updateQuery = @"
                    UPDATE [dbo].[Tbl_Blog]
                       SET [BlogTitle] = @BlogTitle
                          ,[BlogAuthor] = @BlogAuthor
                          ,[BlogContent] = @BlogContent
                          ,[DeleteFlag] = 0
                     WHERE BlogId = @BlogId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@BlogId", id);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count == 0) return NotFound();
                }

                using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                {
                    updateCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
                    updateCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
                    updateCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
                    updateCommand.Parameters.AddWithValue("@BlogId", id);
                    int rowAffected = updateCommand.ExecuteNonQuery();

                    string message = rowAffected > 0 ? "Updating successful" : "Updating failed";
                    return Ok(message);
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string checkQuery = @"select count(*) from [ENPDotNetBatch5].[dbo].[Tbl_Blog] where BlogId = @BlogId";
            string softDeleteQuery = @"UPDATE [dbo].[Tbl_Blog]
                           SET [DeleteFlag] = 1
                         WHERE BlogId =@BlogId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@BlogId", id);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count == 0) return NotFound();
                }

                using (SqlCommand deleteCommand = new SqlCommand(softDeleteQuery, conn))
                {
                    deleteCommand.Parameters.AddWithValue("@BlogId", id);
                    int rowAffected = deleteCommand.ExecuteNonQuery();
                    string message = rowAffected > 0 ? "Deleting successful" : "Deleting failed";
                    return Ok(message);
                }
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";
            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters !");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result > 0 ? "Updating successful." : "Updating failed");
        }
    }
}
