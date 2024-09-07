namespace WebApplication1.Middlewares;

public class IpAccessMiddleware
{
    private readonly RequestDelegate _next;
    private readonly List<string> _accessIp = new List<string>(){"192.168.68.103"};
    private readonly List<string> _accessPathes = new List<string>(){"/admin"};
    
    public IpAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var path = context.Request.Path;
        if (_accessPathes.Contains(path) && !_accessIp.Contains(ipAddress))
        {
            context.Response.StatusCode = 403;
            context.Response.WriteAsync("Access denied");
            return Task.CompletedTask;
        }
        System.Console.WriteLine($"Request from IP: {ipAddress}");
        return _next(context);
    }
}