using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Web.SPA.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApplicationFiles(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            SetupDefaultFiles(app);
            SetupLocalFiles(app, env);
        }

        private static void SetupDefaultFiles(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
        }

        private static void SetupLocalFiles(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var webFilesPath = env.WebRootPath;
            var configsPath = Path.Combine(new DirectoryInfo(env.ContentRootPath).Parent!.FullName, "Web.Configuration");

            var webFileProvider = new PhysicalFileProvider(webFilesPath);
            var configsFileProvider = new PhysicalFileProvider(configsPath);

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = configsFileProvider,
                RequestPath = "/configs"
            });

            env.WebRootFileProvider = new CompositeFileProvider(webFileProvider, configsFileProvider);
        }
    }
}