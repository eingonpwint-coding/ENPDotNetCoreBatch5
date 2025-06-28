// See https://aka.ms/new-console-template for more information
using ENPDotNetCoreBatch5.Database.Models;
using Newtonsoft.Json;

//Console.WriteLine("Hello, World!");
//AppDbContext db = new AppDbContext();
//var lst = db.TblBlogs.ToList();
//foreach (var item in lst)
//{
//    Console.WriteLine(item.BlogId);
//    Console.WriteLine(item.BlogTitle);
//    Console.WriteLine(item.BlogAuthor);
//    Console.WriteLine(item.BlogContent);
//}

var blog = new BlogModel
{
    Id = 1,
    Title = "Test Title",
    Author = "Test Author",
    Content = "Test Content",
};
// Encode, Decode, Encryption, Decryption

//string jsonStr = JsonConvert.SerializeObject(blog,Formatting.Indented);// C# to json
string jsonStr = blog.ToJason();

Console.WriteLine(jsonStr);


string jsonStr2 = """{"id":1,"title":"Test Title","author":"Test Author","content":"Test Content"}""";
var blog2 = JsonConvert.DeserializeObject<BlogModel>(jsonStr2);

//System.Text.Json.JsonSerializer.Serialize(blog);
//System.Text.Json.JsonSerializer.Deserialize<BlogModel>(jsonStr2);

Console.WriteLine(blog2.Title);

Console.ReadLine();
public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

public static class Extension //DevCode
{
    public static string ToJason(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);// C# to json
        return jsonStr;
    }
}