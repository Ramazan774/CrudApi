var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var blogs = new List<Blog>
{
  new Blog { Title = "My First Post", Body = "This is my first post"},
  new Blog { Title = "My Second Post", Body = "This is my second post"}
};

app.MapGet("/", () => "I am root!");

app.MapGet("/blogs", () => {
  return blogs;
});

app.MapGet("/blogs/{id}", (int id) =>{
  if (id < 0 || id >= blogs.Count) {
    return Results.NotFound();
  } else {
    return Results.Ok(blogs[id]);
  }
  
});
app.Run();

public class Blog
{
  public required string Title { get; set; }
  public required string Body { get; set; }
}
