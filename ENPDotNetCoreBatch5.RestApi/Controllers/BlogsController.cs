using ENPDotNetCoreBatch5.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ENPDotNetCoreBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetBLogs()
        {
            var lst = _db.TblBlogs.AsNoTracking().Where(x=>x.DeleteFlag ==false).ToList();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok(blog);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog tblBlog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return NotFound();
            }

            item.BlogTitle = tblBlog.BlogTitle;
            item.BlogAuthor = tblBlog.BlogAuthor;
            item.BlogContent = tblBlog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return NotFound();
            }

            item.DeleteFlag = true;
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return NotFound();

            }

            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle= blog.BlogTitle;
            }

            if(!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor= blog.BlogAuthor;
            }

            if(!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent= blog.BlogContent;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }
    }
}
