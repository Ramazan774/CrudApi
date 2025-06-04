var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging((o)=>{});

var app = builder.Build();

var blogs = new List<Blog>
{
  new Blog { Title = "My First Post", Body = "This is my first post"},
  new Blog { Title = "My Second Post", Body = "This is my second post"}
};

//app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();
//app.UseExceptionHandler();


// app.UseHttpLogging();
app.Use(async (context, next) => {
  Console.WriteLine("Logic before");
  await next.Invoke();
  Console.WriteLine("Logic after");
});
app.MapGet("/", () => "I am root!");
app.MapGet("/hello", () => "This is hello root!");

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

app.MapPost("/blogs", (Blog blog) => {
  blogs.Add(blog);
  return Results.Created($"/blogs/{blogs.Count -1}", blog);
});

app.MapDelete("/blogs/{id}", (int id) => {
  if (id < 0 || id >= blogs.Count) {
    return Results.NotFound();
  } else {
    // var blog = blogs[id];
    blogs.RemoveAt(id);
    return Results.NoContent();
  }
});

app.MapPut("/blogs/{id}", (int id, Blog blog) => {
  if (id < 0 || id >= blogs.Count) {
    return Results.NotFound();
  } else {
    blogs[id] = blog;
    return Results.Ok(blog);
  }
});

app.Run();

public class Blog
{
  public required string Title { get; set; }
  public required string Body { get; set; }
}
