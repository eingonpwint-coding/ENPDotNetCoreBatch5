using Dapper;
using ENPDotNetCoreBatch5.Database.Models;
using ENPDotNetCoreBatch5.RestApi.DataModels;
using ENPDotNetCoreBatch5.RestApi.VIewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;

namespace ENPDotNetCoreBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=ENPDotNetBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        [HttpGet]
        public IActionResult GetBLogs()
        {
            List<BlogViewModel> blogs = new List<BlogViewModel>();
            string query = "select * from Tbl_Blog";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
                foreach (BlogDataModel b in lst)
                {
                    var item = new BlogViewModel
                    {
                        Id = b.BlogId,
                        Title = b.BlogTitle,
                        Author = b.BlogAuthor,
                        Content = b.BlogContent
                    };
                    blogs.Add(item);
                }
                return Ok(blogs);
            }
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogViewModel blog)
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
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content
                });
                return Ok(result > 0 ? "Creating successfully" : "Creating failed");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_Blog where DeleteFlag=0 and BlogId = @BlogId;";
                var item = db.Query<BlogDataModel>(query, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();
                if (item is null) return NotFound();

                return Ok(item);
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string checkQuery = "select * from tbl_Blog where DeleteFlag=0 and BlogId = @BlogId;";
                var item = db.Query<BlogDataModel>(checkQuery, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();
                if (item is null) return NotFound();


                string updateQuery = @" UPDATE [dbo].[Tbl_Blog]
                       SET [BlogTitle] = @BlogTitle
                          ,[BlogAuthor] = @BlogAuthor
                          ,[BlogContent] = @BlogContent
                          ,[DeleteFlag] = 0
                     WHERE BlogId = @BlogId;";

                var result = db.Execute(updateQuery, new BlogDataModel
                {
                    BlogId = id,
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content,
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string checkQuery = "select * from tbl_Blog where DeleteFlag=0 and BlogId = @BlogId;";
                var item = db.Query<BlogDataModel>(checkQuery, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();
                if (item is null) return NotFound();


                string softDeleteQuery = @" UPDATE [dbo].[Tbl_Blog]
                       SET [DeleteFlag] = 1
                     WHERE BlogId = @BlogId;";

                var result = db.Execute(softDeleteQuery, new BlogDataModel
                {
                    BlogId = id,
                });
                return Ok(result == 1 ? "Deleting Successful" : "Deleting Failed");
            }

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string condition = "";
            if(!string.IsNullOrEmpty(blog.Title))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
            }

            if(!string.IsNullOrEmpty(blog.Author))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
            }

            if(!string.IsNullOrEmpty(blog.Content))
            {
                condition += " [BlogContent] = @BlogContent, ";
            }

            if(condition.Length == 0)
            {
                return BadRequest("Invalid parameters");
            }

            condition = condition.Substring(0, condition.Length - 2);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string checkQuery = "select * from tbl_Blog where DeleteFlag=0 and BlogId = @BlogId;";
                var item = db.Query<BlogDataModel>(checkQuery, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();
                if (item is null) return NotFound();


                string updateQuery = $@" UPDATE [dbo].[Tbl_Blog]
                       SET {condition}
                     WHERE BlogId = @BlogId;";

                var result = db.Execute(updateQuery, new BlogDataModel
                {
                    BlogId = id,
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content,
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
            }
        }

    }   
}

