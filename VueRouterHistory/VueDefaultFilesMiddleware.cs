using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VueRouterHistory
{
    public class VueDefaultFilesMiddleware
    {
        private readonly DefaultFilesOptions _options;
        private readonly PathString _matchUrl;
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _hostingEnv;

        public VueDefaultFilesMiddleware(RequestDelegate next, IWebHostEnvironment hostingEnv, IOptions<DefaultFilesOptions> options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (hostingEnv == null)
            {
                throw new ArgumentNullException(nameof(hostingEnv));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next;
            _options = options.Value;
            _hostingEnv = hostingEnv;
            _matchUrl = _options.RequestPath;
        }

        internal static bool IsGetOrHeadMethod(string method)
        {
            return HttpMethods.IsGet(method) || HttpMethods.IsHead(method);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.GetEndpoint() == null && IsGetOrHeadMethod(context.Request.Method))
            {
                var path = Path.Join(_hostingEnv.WebRootPath, context.Request.Path);
                var name = Path.Combine(Path.GetDirectoryName(path)!, "index.html");
                if (File.Exists(name))
                {
                    context.Response.StatusCode = 200;
                    await context.Response.SendFileAsync(name);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
