using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var userService = new UserCervice();
var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
var template = fileProvider.GetFileInfo("html/index.html");
string templateString = await File.ReadAllTextAsync(template.PhysicalPath);
string contentTag = "<!--content-->";
string scriptsTag = "<!--scripts-->";


app.UseStaticFiles();


app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    context.Response.SendFileAsync(template);
});
app.Run();


//var configService = app.Services.GetRequiredService<IConfiguration>();
//var connectionString = configService.GetConnectionString("DefaultConnection:ConnectionStrings");

//app.Run(async (context) =>
//{
//    var response = context.Response;
//    var request = context.Request;
//    response.ContentType = "text/html; charset=utf-8";

//    //При переходе на главную страницу, считываем всех пользователей
//    if (request.Path == "/")
//    {
//        List<User> users = new List<User>();
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            await connection.OpenAsync();
//            SqlCommand command = new SqlCommand("select Id, Name, Age from Users", connection);
//            using (SqlDataReader reader = await command.ExecuteReaderAsync())
//            {
//                if (reader.HasRows)
//                {
//                    while (await reader.ReadAsync())
//                    {
//                        users.Add(new User(reader.GetString(0), reader.GetString(1), reader.GetInt32(2)));
//                    }
//                }
//            }
//        }
//        await response.WriteAsync(GenerateHtmlPage(BuildHtmlTable(users), "All Users from DataBase"));
//    }
//    else
//    {
//        response.StatusCode = 404;
//        await response.WriteAsJsonAsync("Page Not Found");
//    }
//});
//app.Run();

//static string GenerateHtmlPage(string body, string header)
//{
//    string html = $"""
//        <!DOCTYPE html>
//        <html>
//        <head>
//            <meta charset="utf-8" />
//            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" 
//            integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
//            <title>{header}</title>
//        </head>
//        <body>
//        <div class="container">
//        <h2 class="d-flex justify-content-center">{header}</h2>
//        <div class="mt-5"></div>
//        {body}
//            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"
//            integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
//        </div>
//        </body>
//        </html>
//        """;
//    return html;
//}
//static string ToTable(List<User> users)
//{
//    StringBuilder st = new StringBuilder("<table class=\"table\"><tr><th>Id</th><th>Name</th><th>Age</th></tr>");
//    foreach (User user in users)
//    {
//        st.Append($"<tr><td>{user.Id}</td><td>{user.Name}</td><td>{user.Age}</td></tr>");
//    }
//    st.Append("</table>");
//    return st.ToString();
//}

//static string BuildHtmlTable<T>(IEnumerable<T> collection)
//{
//    StringBuilder tableHtml = new StringBuilder();
//    tableHtml.Append("<table>");

//    PropertyInfo[] properties = typeof(T).GetProperties();

//    tableHtml.Append("<tr>");
//    foreach (PropertyInfo property in properties)
//    {
//        tableHtml.Append($"<th>{property.Name}</th>");
//    }
//    tableHtml.Append("</tr>");

//    foreach (T item in collection)
//    {
//        tableHtml.Append("<tr>");
//        foreach (PropertyInfo property in properties)
//        {
//            object value = property.GetValue(item);
//            tableHtml.Append($"<td>{value}</td>");
//        }
//        tableHtml.Append("</tr>");
//    }

//    tableHtml.Append("</table>");
//    return tableHtml.ToString();
//}
//record User(string Id, string Name, int Age)
//{
//    public User(string name, int age) : this(Guid.NewGuid().ToString(), name, age) { }
//}