using ENPDotNetCoreBatch5.Database.Models;
using ENPDotNetCoreBatch5.Domain.Features.Blog;
using ENPDotNetCoreBatch5.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;

namespace ENPDotNetCoreBatch5.MvcApp.Controllers
{
	public class BlogAjaxController : Controller
	{
		private readonly BlogService _blogService;

		public BlogAjaxController(BlogService blogService)
		{
			_blogService = blogService;
		}
		//crud
		//Read

		[ActionName("Index")]
		public IActionResult BlogList()
		{
			//var lst = _blogService.GetBlogs();
			//return View("BlogList", lst
			return View("BlogList");
		}

		[ActionName("List")]
		public IActionResult BlogListAjax()
		{
			var lst = _blogService.GetBlogs();
			return Json(lst);
		}

		[ActionName("Create")]
		public IActionResult BlogCreate()
		{
			return View("BlogCreate");
		}

		[HttpPost]
		[ActionName("Save")]
		public IActionResult BlogSave(BlogRequestModel requestModel)
		{
			MessageModel model;
			try
			{
				_blogService.CreateBlog(new TblBlog
				{
					BlogAuthor = requestModel.Author,
					BlogContent = requestModel.Content,
					BlogTitle = requestModel.Title
				});

				TempData["IsSuccess"] = true;
				TempData["Message"] = "Blog Created Successfully";

				model = new MessageModel(true, "Blog Created Successfully");
			}
			catch (Exception ex)
			{
				TempData["IsSuccess"] = false;
				TempData["Message"] = ex.ToString();

				model = new MessageModel(false, ex.ToString());
			}

			return Json(model);
		}

		[ActionName("Edit")]
		public IActionResult BlogEdit(int id)
		{
			var blog = _blogService.GetBlog(id);

			BlogRequestModel blogRequestModel = new BlogRequestModel
			{
				Id = blog.BlogId,
				Author = blog.BlogAuthor,
				Content = blog.BlogContent,
				Title = blog.BlogTitle,
			};

			return View("BlogEdit", blogRequestModel);
		}

		[HttpPost]
		[ActionName("Update")]
		public IActionResult BlogUpdate(int id, BlogRequestModel requestModel)
		{
			MessageModel model;
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
				model = new MessageModel(true, "Blog Updated Successfully");
			}
			catch (Exception ex)
			{
				TempData["IsSuccess"] = false;
				TempData["Message"] = ex.ToString();

				model = new MessageModel(false, ex.ToString());
			}

			return Json(model);
		}

		[HttpPost]
		[ActionName("Delete")]
		public IActionResult BlogDelete(BlogRequestModel requestModel)
		{
			MessageModel model;
			try
			{
				_blogService.DeleteBlog(requestModel.Id);
				model = new MessageModel(true, "Blog Deleted Successfully");
			}
			catch (Exception ex)
			{
				TempData["IsSuccess"] = false;
				TempData["Message"] = ex.ToString();
				model = new MessageModel(false, ex.ToString());
			}
			return Json(model);
		}
	}


	public class MessageModel
	{
		public MessageModel()
		{
		}
		public MessageModel(bool isSuccess, string message)
		{
			IsSuccess = isSuccess;
			Message = message;
		}

		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}

}