namespace WebApplication1.Middlewares;

public class IpAddressMiddleware
{
    
    private readonly RequestDelegate _next;
    private  Dictionary<string, IpData> _ipData = new Dictionary<string, IpData>();

    public IpAddressMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        if (!_ipData.ContainsKey(ipAddress))
        {
            _ipData.Add(ipAddress, new IpData(ipAddress));
        }
        else
        {
            _ipData[ipAddress].AddQuantity();
        }
        
        if(_ipData[ipAddress].IsBlocked)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("You are blocked for 1 hour");
            return;
        }
        System.Console.WriteLine($"Request from IP: {ipAddress}");

        await _next(context);
    }
}

public class IpData
{
    public int Quantity { get; set; }
    public string Ip { get; set; }
    public DateTime? AccessDate { get; set; }
    public bool IsBlocked { get; set; }
    
    public IpData(string ip)
    {
        Ip = ip;
        Quantity = 1;
        AccessDate = null;
        IsBlocked = false;
    }

    public void AddQuantity()
    {
        
        if(IsBlocked && AccessDate < DateTime.Now)
        {
            IsBlocked = false;
            Quantity = 1;
            return;
        }
        
        if(IsBlocked && AccessDate > DateTime.Now)
        {
            return;
        }
        
        
        if (Quantity <= 99){
            Quantity++;
        }
        else
        {
            IsBlocked = true;
            AccessDate = DateTime.Now.AddHours(1);
        }
    }

    
}