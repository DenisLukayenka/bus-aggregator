using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Web.SPA.Extensions
{
    using Web.Infrastructure.Configuration;
    using Web.Infrastructure.Exceptions;
    using Web.Infrastructure.Models.MapInfo;
    using Web.Infrastructure.Repository;

    public static class ApplicationBuilderExtensions
    {
        public static void UseRepositories(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;

                var mapRepository = provider.GetRequiredService<IRepository<Map>>();
                mapRepository.Init();
            }
        }

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
            if (app.ApplicationServices.GetService(typeof(AppConfiguration)) is not AppConfiguration config)
                throw new ConfigurationException("Cannot initialize configuration instance");

            var webFileProvider = new PhysicalFileProvider(config.WebRootPath);
            var configsFileProvider = new PhysicalFileProvider(config.ConfigsFullPath);
            var localesFileProvider = new PhysicalFileProvider(config.LocalesFolderFullPath);

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = configsFileProvider,
                RequestPath = "/maps"
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = localesFileProvider,
                RequestPath = "/locales"
            });

            env.WebRootFileProvider = new CompositeFileProvider(webFileProvider, configsFileProvider, localesFileProvider);
        }
    }
}