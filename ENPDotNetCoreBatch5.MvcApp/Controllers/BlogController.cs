using ENPDotNetCoreBatch5.Database.Models;
using ENPDotNetCoreBatch5.Domain.Features.Blog;
using ENPDotNetCoreBatch5.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ENPDotNetCoreBatch5.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var lst = _blogService.GetBlogs();
            return View(lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogRequestModel model)
        {
            try
            {
                _blogService.CreateBlog(new TblBlog
                {
                    BlogTitle = model.Title,
                    BlogAuthor = model.Author,
                    BlogContent = model.Content
                });
                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Created Successfully";
            }
            catch (Exception ex)
            {
                //ViewBag.IsSuccess = false;
                //ViewBag.Message = ex.ToString();

                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }

            return RedirectToAction("Index");

        }

       
        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {
            _blogService.DeleteBlog(id);
            return RedirectToAction("Index");
        }

		[ActionName("Edit")]
		public IActionResult BlogEdit(int id)
        {
            var blog = _blogService.GetBlog(id);
            BlogRequestModel model = new BlogRequestModel
            {
                Id = blog.BlogId,
                Title = blog.BlogTitle,
                Author = blog.BlogAuthor,
                Content = blog.BlogContent
            };
            return View("BlogEdit",model);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogRequestModel requestModel)
        {
			try
			{
				_blogService.UpdateBlog(id, new TblBlog
				{
					BlogAuthor = requestModel.Author,
					BlogContent = requestModel.Content,
					BlogTitle = requestModel.Title
				});

				TempData["IsSuccess"] = true;
				TempData["Message"] = "Blog Updated Successfully";
			}
			catch (Exception ex)
			{
				TempData["IsSuccess"] = false;
				TempData["Message"] = ex.ToString();
			}

			return RedirectToAction("Index");
		}
	        
    }
}
