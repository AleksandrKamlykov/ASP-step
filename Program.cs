
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Context;
using WebApplication1.Middlewares;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataBaseContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
var scope = app.Services.CreateScope();
var carContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

var usersAuth = new Dictionary<string, string>();


var htmlTemplate = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "html", "index.html"));

app.UseMiddleware<IpAddressMiddleware>();
app.UseMiddleware<IpAccessMiddleware>();
app.UseMiddleware<AuthMiddleware>(usersAuth);

app.MapGet("/",async (context) =>
{
   context.Response.ContentType = "text/html";


   StringBuilder content = new StringBuilder(htmlTemplate);
   
   await context.Response.WriteAsync(content.ToString());
});


app.MapGet("api/currencies",async (context) =>
{
   var currencies = await carContext.Currencies.ToListAsync();
   
   context.Response.ContentType = "application/json; charset=utf-8";
   await context.Response.WriteAsJsonAsync(currencies);
});

app.MapGet("api/currencies/calcualte",async (context) =>
{
   var currencies = await carContext.Currencies.ToListAsync();
   var currencyFrom = currencies.FirstOrDefault(c => c.Cc == context.Request.Query["fromCc"]);
   var currencyTo = currencies.FirstOrDefault(c => c.Cc == context.Request.Query["toCc"]);
   var amount = float.Parse(context.Request.Query["amount"]);
   
   context.Response.ContentType = "application/json";
   await context.Response.WriteAsJsonAsync(new {result = amount * currencyFrom.Rate / currencyTo.Rate, currencyTo.Cc, currencyTo.Text});
});

app.MapGet("api/books",async (context) =>
{
   
   if(context.Request.Query.ContainsKey("category") && !string.IsNullOrEmpty(context.Request.Query["category"]))
   {
      var category = context.Request.Query["category"];
      var books = await carContext.Books.Where(b => b.Category.ToLower().Contains(category.ToString().ToLower())).ToListAsync();
      context.Response.ContentType = "application/json; charset=utf-8";
      await context.Response.WriteAsJsonAsync(books);
      return;
   }
   else
   {
      var books = await carContext.Books.ToListAsync();
   
      context.Response.ContentType = "application/json; charset=utf-8";
      await context.Response.WriteAsJsonAsync(books); 
   }
   

});

app.MapGet("/home",async (context) =>
{
   context.Response.ContentType = "text/html";
   var page = new Page();
   page.AddHeader("header.html");
   
   await context.Response.WriteAsync(page.GetContent());
});

app.MapGet("/exchange",async (context) =>
{
   context.Response.ContentType = "text/html";
   var page = new Page();
   page.AddHeader("header.html");
   page.AddContent("currency.html");
   page.AddScripts("currency.js");
   
   await context.Response.WriteAsync(page.GetContent());
});

app.MapGet("/books",async (context) =>
{
   context.Response.ContentType = "text/html";
   var page = new Page();
   page.AddHeader("header.html");
   page.AddContent("books.html");
   page.AddScripts("books.js");
   
   await context.Response.WriteAsync(page.GetContent());
});

app.MapGet("/auth",async (context) =>
{
   context.Response.ContentType = "text/html";
   await context.Response.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "html", "401.html"));
});

app.MapGet("/api/auth",async (context) =>
{
 var name = context.Request.Query["name"];
   var email = context.Request.Query["email"];
   
   if (name == "admin" && email == "admin@email.com")
   {
       var key = Guid.NewGuid().ToString();
       usersAuth.Add(key, name);
       context.Response.Cookies.Append("AuthCookie", key, new CookieOptions
       {
           HttpOnly = true,
           Secure = true,
           SameSite = SameSiteMode.Strict
       });
       context.Response.StatusCode = 200;
       context.Response.Redirect("/home");
   }
   else
   {
       context.Response.StatusCode = 401;
   }
});

app.Run();
