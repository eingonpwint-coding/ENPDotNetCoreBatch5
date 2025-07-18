using ENPDotNetCoreBatch5.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace ENPDotNetCoreBatch5.MinimalApi.Endpoints.Blog;

public static class BlogEndpoint
{
    //extension method
    //    public static string Test(this int i)
    //    {
    //        return i.ToString();
    //    }
    //}

    public static void UseBlogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
        {
            
            var lst = db.TblBlogs.AsNoTracking().ToList();
            return Results.Ok(lst);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] AppDbContext db,int id) =>
        {
           
            var item = db.TblBlogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.NotFound("No data found");
            }
            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();


        app.MapPost("/blogs", ([FromServices] AppDbContext db,TblBlog blog) =>
        {
           
            db.TblBlogs.Add(blog);
            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();


        app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db,int id, TblBlog blog) =>
        {
            
            var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.NotFound("No data found");

            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();


        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            
            var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.NotFound("No data found");

            }
            db.Entry(item).State = EntityState.Deleted;
            db.SaveChanges();
            return Results.Ok();
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}
