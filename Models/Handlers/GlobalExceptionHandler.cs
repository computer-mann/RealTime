using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RealTime.Models.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "text/html";
            var contents=File.ReadAllText("wwwroot/error/Error.html");
            //await httpContext.Response.WriteAsync(contents);
            await httpContext.Response.SendFileAsync("wwwroot/error/Error.html");
            return true;
        }
    }
}
