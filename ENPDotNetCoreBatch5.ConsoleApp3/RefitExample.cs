using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENPDotNetCoreBatch5.ConsoleApp3
{
    public  class RefitExample
    {
        public async Task Run()
        {

            //Get all lists
            var blogApi = RestService.For<IBlogApi>("https://localhost:7082");
            var lst = await blogApi.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogTitle);
            }

            //Get By id
            var item2 = await blogApi.GetBlog(1);
            try
            {
                var item3 = await blogApi.GetBlog(100);
            }
            catch(ApiException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No data found");
                }
            }

            //Create Blog
            var item4 = await blogApi.CreateBlog(new BlogModel
            {
                BlogTitle = "test",
                BlogAuthor = "test",
                BlogContent = "test",
            });

            //Patch BLog
            try
            {
                var item5 = await blogApi.PatchBlog(1027, new BlogModel
                {
                    BlogTitle = "testing changed",
                    BlogAuthor = "",
                    BlogContent = "",
                });

                Console.WriteLine("Blog updated successfully.");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    Console.WriteLine("Blog not found.");
                else
                    Console.WriteLine($"Error: {ex.StatusCode}");
            }

            //Delete Blog
            try
            {
                await blogApi.DeleteBlog(1025);
                Console.WriteLine("Blog deleted successfully.");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Blog not found.");
                }
                else
                {
                    Console.WriteLine($"Error: {ex.StatusCode}");
                }
            }

            //Update Blog
            try
            {
                var item5 = await blogApi.UpdateBlog(1028, new BlogModel
                {
                    BlogTitle = "testing changed",
                    BlogAuthor = "",
                    BlogContent = ""
                });
                Console.WriteLine("Blog updated successfully.");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    Console.WriteLine("Blog not found.");
                else
                    Console.WriteLine($"Error: {ex.StatusCode}");
            }

        }
         
    }
}

