using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueRouterHistory;

namespace Microsoft.AspNetCore.Builder
{
    public static class DefaultFilesExtensions
    {
        /// <summary>
        /// Enables default file mapping on the current path
        /// Should invoke after app.UseStaticFiles()
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRouterHistory(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<DefaultFilesMiddleware>();
        }

        /// <summary>
        /// Enables default file mapping on the current path
        /// Should invoke after app.UseStaticFiles()
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRouterHistory(this IApplicationBuilder app, DefaultFilesOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<DefaultFilesMiddleware>(Options.Create(options));
        }
    }
}
