using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var userService = new UserCervice();
var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
var template = fileProvider.GetFileInfo("html/index.html");
var contentTag = "<!-- content -->";


app.UseStaticFiles();

// Default route
app.MapGet("/",async (context) =>
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

// Get all users
app.MapGet("/api/customers", async (context) =>
{


    var formFile = fileProvider.GetFileInfo("html/form.html");

    var content = await File.ReadAllTextAsync(template.PhysicalPath);


    var tableFile = fileProvider.GetFileInfo("html/table.html");
    string tableHTML = await File.ReadAllTextAsync(tableFile.PhysicalPath);
    

    var users =  userService.GetUsers();

    tableHTML = tableHTML.Replace("<!-- table -->", string.Join("", users.Select(u => $"<tr><td>{u.Name}</td><td>{u.Email}</td><td>{u.Phone}</td></tr>")));
    content = content.Replace(contentTag, tableHTML);
    
    if (template.Exists)
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

// Register user
app.MapGet("/register", async (context) =>
{


    var query = context.Request.Query;
    string name = query["name"];
    string email = query["email"];
    string phone = query["phone"];

    if (new List<string>(){ name,email,phone}.Contains(String.Empty))
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("File not found.");
    }

    var user = new User
    {
        Name = name,
        Email = email,
        Phone = phone
    };
    userService.AddUser(user);


    var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
    var content = await File.ReadAllTextAsync(template.PhysicalPath);
    content = content.Replace(contentTag, "<h1>Registration successful</h1>");



  
    context.Response.ContentType = "text/html";

    await context.Response.WriteAsync(content);
 
});

// Calculate length of string
app.MapGet("/api/length/{content}", async (context) =>
{
    var content = context.Request.RouteValues["content"]?.ToString();
    if (content == null)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync("Length parameter is missing.");
        return;
    }

    await context.Response.WriteAsync($"Length: {content.Length}; content: {content}");
});

// Calculate area of circle
app.MapGet("/api/circlearea", async (context) =>
{

    int radius = 0;
    if (!int.TryParse(context.Request.Query["radius"], out radius) || radius <= 0)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync("Radius parameter is missing or invalid.");
        return;
    }

    double area = Math.PI * Math.Pow(radius, 2);
    await context.Response.WriteAsync($"Area of circle with radius {radius} is {area}");
});

// Show all query parameters [key: value]
app.MapGet("/api/queries", async (context) =>
{

    var queryParams = context.Request.Query;
    string response = string.Join("\n", queryParams.Select(q => $"{q.Key}: {q.Value}"));

    await context.Response.WriteAsync(response);
});


app.Run();
