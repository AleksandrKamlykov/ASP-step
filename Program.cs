using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Interfaces;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserService, UserCervice>();
builder.Services.AddSingleton<IBookService, BookService>();

var userService = builder.Services.BuildServiceProvider().GetService<IUserService>();
var bookService = builder.Services.BuildServiceProvider().GetService<IBookService>();
var app = builder.Build();


var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
var template = fileProvider.GetFileInfo("html/index.html");
var contentTag = "<!-- content -->";


app.UseStaticFiles();

// Default route
app.MapGet("/", async (context) =>
{

    var formFile = fileProvider.GetFileInfo("html/form.html");

    var content = await File.ReadAllTextAsync(template.PhysicalPath);
    content = content.Replace(contentTag, await File.ReadAllTextAsync(formFile.PhysicalPath));
    if (template.Exists && formFile.Exists)
    {
        context.Response.ContentType = "text/html";

        await context.Response.WriteAsync(content);
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("File not found.");
    }
});

app.MapGet("form", async (context) =>
{
    var formFile = fileProvider.GetFileInfo("html/form.html");

    if (formFile.Exists)
    {
        context.Response.ContentType = "text/html";

        await context.Response.SendFileAsync(formFile);
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("File not found.");
    }
});

app.MapGet("/users", async (context) =>
{
    var usersFile = fileProvider.GetFileInfo("html/users.html");

  

    if (usersFile.Exists)
    {
        context.Response.ContentType = "text/html";

        await context.Response.SendFileAsync(usersFile);
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("File not found.");
    }
});

// Route to save user
app.MapPost("/save", async (context) =>
{
    var user = new User
    {
        Name = context.Request.Form["name"],
        Email = context.Request.Form["email"],
        Phone = context.Request.Form["phone"]
    };

    userService.AddUser(user);

    context.Response.Redirect("/");
});

app.MapGet("/books", async (context) =>
{
    var booksFile = fileProvider.GetFileInfo("html/books.html");



    if (booksFile.Exists)
    {
        context.Response.ContentType = "text/html";

        await context.Response.SendFileAsync(booksFile);
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("File not found.");
    }
});

// Route to list users
app.MapGet("api/users", async (context) =>
{
    var users = userService.GetUsers();


    context.Response.ContentType = "Application/json";
    await context.Response.WriteAsJsonAsync(users);
});

app.MapGet("api/users/{id}", async (context) =>
{
    var id = Guid.Parse(context.Request.RouteValues["id"].ToString());
    var user = userService.GetUserById(id);

    if (user != null)
    {
        context.Response.ContentType = "Application/json";
        await context.Response.WriteAsJsonAsync(user);
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("User not found.");
    }
});

// Route to remove user
app.MapDelete("api/users/{id}", async (context) =>
{
    var id = Guid.Parse(context.Request.RouteValues["id"].ToString());
    var user = userService.GetUserById(id);

    if (user != null)
    {
        userService.RemoveUser(user);
        
        context.Response.WriteAsJsonAsync(user);
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("User not found.");
    }
});

// Route to update user
app.MapPut("api/users/{id}", async (context) =>
{
    var user = await context.Request.ReadFromJsonAsync<User>();

   if(user != null)
    {
        userService.UpdateUser(user);
        context.Response.Redirect("/users");
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("User not found.");
    }

});

app.MapGet("/api/books", async (context) =>
{
    context.Response.ContentType = "Application/json";

    var books = bookService.GetBooks();

    var Query = context.Request.Query;

    if(Query.ContainsKey("key") && Query.ContainsKey("value"))
    {
        var value = Query["value"].ToString().ToLower();
        var key = Query["key"].ToString().ToLower();

        if(string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
        {
            await context.Response.WriteAsJsonAsync(books);

        }

        if (key == "title")
        {
            books = books.Where(b => b.Title.ToLower().Contains(value)).ToList();
        }else
        {
            books = books.Where(b => b.ISBN.ToLower().Contains(value)).ToList();
        }
    }
    if(Query.ContainsKey("order"))
    {
        var sort = Query["erder"].ToString().ToLower();
        if(sort == "asc")
        {
            books = books.OrderBy(b => b.Title).ToList();
        }else
        {
            books = books.OrderByDescending(b => b.Title).ToList();
        }
    }

    await context.Response.WriteAsJsonAsync(books);

});


app.Run();
