using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient();
builder.Services.AddSingleton<WeatherService>();

var app = builder.Build();

var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
var template = fileProvider.GetFileInfo("html/index.html");
var templateString = await File.ReadAllTextAsync(template.PhysicalPath);


var contentTag = "<!--content-->";
var scriptsTag = "<!--scripts-->";

var templateCars = templateString.Replace(scriptsTag, "<script src='/js/cars.js'></script>");


app.UseStaticFiles();

var scope = app.Services.CreateScope();
var carContext = scope.ServiceProvider.GetRequiredService<CarsContext>();
var carService = new CarsService(carContext);

app.MapGet("/api/cars", async (HttpContext context) =>
{
    var cars = await carService.GetAllCars();
    context.Response.StatusCode = 200;
    await context.Response.WriteAsJsonAsync(cars);
});

app.MapPost("/api/cars", async (HttpContext context) =>
{
    var car = await context.Request.ReadFromJsonAsync<Car>();
    var res =await carService.AddCar(car);
    context.Response.StatusCode = 201;
    await context.Response.WriteAsJsonAsync(res);
});

app.MapPut("/api/cars", async (HttpContext context) =>
{
    var car = await context.Request.ReadFromJsonAsync<Car>();
    Console.WriteLine(car);
    var res = await carService.UpdateCar(car);
    context.Response.StatusCode = 200;
    await context.Response.WriteAsJsonAsync(res);
});

app.MapDelete("/api/cars/{carId}", async (HttpContext context) =>
{
    var carId = Guid.Parse(context.Request.RouteValues["carId"].ToString());
     await carService.DeleteCar(carId);
    context.Response.StatusCode = 200;
    await context.Response.WriteAsJsonAsync(carId);
});

app.MapGet("/api/cars/{carId}", async (HttpContext context) =>
{
    var carId = Guid.Parse(context.Request.RouteValues["carId"].ToString());
    var car = await carService.GetCarById(carId);
    context.Response.StatusCode = 200;
    await context.Response.WriteAsJsonAsync(car);
});

app.MapGet("/", async (HttpContext context) =>
{
    await context.Response.WriteAsync(templateCars);
});


// -------- STATIS FILES --------
var fileProviderFiles = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files"));

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProviderFiles,
    RequestPath = "/files"
});

// Define a route to handle GET requests for HTML files
app.MapGet("/files/{*filePath}", async (HttpContext context) =>
{
    var filePath = context.Request.Path.Value.TrimStart('/');
    var fullPath = Path.Combine(fileProvider.Root, filePath);

    if (File.Exists(fullPath) && Path.GetExtension(fullPath).Equals(".html", StringComparison.OrdinalIgnoreCase))
    {
        var fileContent = await File.ReadAllTextAsync(fullPath);
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync(fileContent);
    }
    else
    {
        context.Response.StatusCode = 404;
        var errorFilePath = Path.Combine(fileProvider.Root, "404.html");
        if (File.Exists(errorFilePath))
        {
            var errorContent = await File.ReadAllTextAsync(errorFilePath);
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(errorContent);
        }
        else
        {
            await context.Response.WriteAsync("404 Not Found");
        }
    }
});

// -----Weather -----

app.MapGet("/weather", async (HttpContext context) =>
{
    var filePath = Path.Combine(fileProvider.Root, "html","weather.html");
    var fileContent = await File.ReadAllTextAsync(filePath);
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(fileContent);
});

app.MapGet("/api/weather/{city}", async (HttpContext context, WeatherService weatherService, string city) =>
{
    var weatherData = await weatherService.GetWeatherDataAsync(city);
    if (weatherData != null)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(weatherData);
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("City not found");
    }
});


app.Run();
