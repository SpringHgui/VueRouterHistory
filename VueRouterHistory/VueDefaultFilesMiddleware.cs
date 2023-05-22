using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        string[] ignoreFile = new string[] { ".mjs", ".js", ".css" };

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.GetEndpoint() == null && IsGetOrHeadMethod(context.Request.Method))
            {
                var ext = Path.GetExtension(context.Request.Path.Value);
                if (ignoreFile.Contains(ext))
                {
                    await _next(context);
                    return;
                }

                var subDirs = (context.Request.Path.Value ?? string.Empty).Split('/').Where(x => !string.IsNullOrEmpty(x));

                var file = tryGetIndex(context, subDirs.ToArray(), subDirs.Count());
                if (file != null)
                {
                    string contentType;
                    new FileExtensionContentTypeProvider().TryGetContentType(file, out contentType);
                    context.Response.Headers.Add("Content-Type", contentType ?? "application/octet-stream");
                    await context.Response.SendFileAsync(file);
                    return;
                }
            }

            await _next(context);
        }

        private string tryGetIndex(HttpContext context, string[] subDirs, int count)
        {
            string path = _hostingEnv.WebRootPath;
            for (int i = 0; i < count; i++)
            {
                path = Path.Combine(_hostingEnv.WebRootPath, subDirs[i]);
            }

            if (File.Exists(path))
            {
                return null;
            }

            var name = Path.Combine(path, "index.html");
            if (File.Exists(name))
            {
                return name;
            }

            if (count-- == 0)
                return null;

            return tryGetIndex(context, subDirs, count--);
        }
    }
}
