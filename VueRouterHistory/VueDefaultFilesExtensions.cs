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
    public static class VueDefaultFilesExtensions
    {
        /// <summary>
        /// Enables default file mapping on the current path
        /// Should invoke after app.UseStaticFiles()
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseVueRouterHistory(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<VueDefaultFilesMiddleware>();
        }

        /// <summary>
        /// Enables default file mapping on the current path
        /// Should invoke after app.UseStaticFiles()
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseVueRouterHistory(this IApplicationBuilder app, DefaultFilesOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<VueDefaultFilesMiddleware>(Options.Create(options));
        }
    }
}
