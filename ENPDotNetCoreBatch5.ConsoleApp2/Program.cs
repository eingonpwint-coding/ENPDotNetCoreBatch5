// See https://aka.ms/new-console-template for more information
using ENPDotNetCoreBatch5.Database.Models;

Console.WriteLine("Hello, World!");
AppDbContext db = new AppDbContext();
var lst = db.TblBlogs.ToList();
foreach (var item in lst)
{
    Console.WriteLine(item.BlogId);
    Console.WriteLine(item.BlogTitle);
    Console.WriteLine(item.BlogAuthor);
    Console.WriteLine(item.BlogContent);
}