using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BuditelIntranet
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public MaintenanceMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            bool isMaintenanceMode = _configuration.GetValue<bool>("MaintenanceMode:Enabled");

            if (isMaintenanceMode)
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "maintenance.html"));
                return;
            }

            await _next(context);
        }
    }

}
