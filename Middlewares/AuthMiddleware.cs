using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Dictionary<string, string> _usersAuth;

        public AuthMiddleware(RequestDelegate next, Dictionary<string, string> usersAuth)
        {
            _next = next;
            _usersAuth = usersAuth;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if(context.Request.Path.StartsWithSegments("/api/auth") || context.Request.Path.StartsWithSegments("/auth"))
            {
                await _next(context);
                return;
            }
            
            var userReq = context.Request.Cookies["AuthCookie"] ?? String.Empty;
            if (!_usersAuth.ContainsKey(userReq))
            {
                context.Response.StatusCode = 401;
                 context.Response.Redirect("/auth");
            }
            else
            {
             

                await _next(context);
            }
        }
    }
}