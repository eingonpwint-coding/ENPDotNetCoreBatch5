using ENPDotNetCoreBatch5.Database.Models;
using ENPDotNetCoreBatch5.Domain.Features.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENPDotNetCoreBatch5.RestApi.Controllers
{
    //Presentation Layer
    
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly BlogService _service;

        public BlogServiceController(BlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetBLogs()
        {
            var lst = _service.GetBlogs();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            var model = _service.CreateBlog(blog);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _service.GetBlog(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog tblBlog)
        {
            var item =_service.UpdateBlog(id,tblBlog);
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _service.DeleteBlog(id);
            if (item is null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var item = _service.PatchBlog(id,blog);
            if (item == null)
            {
                return NotFound();

            }

            return Ok(item);
        }
    }
}
