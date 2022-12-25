using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Configuration;
using Web.Infrastructure.Exceptions;
using Web.Infrastructure.Models.Country;
using Web.SPA.Providers;

namespace Web.SPA.Extensions;

public static class ServiceCollectionExtensions
{
    public const string CURRENT_COUNTRY_CONFIG_KEY = "CurrentCountry";

    public static void AddAppConfiguration(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddSingleton<AppConfiguration>(provider =>
        {
            using (var scope = provider.CreateScope())
            {
                var servicesProvider = scope.ServiceProvider;
                var configuration = servicesProvider.GetRequiredService<IConfiguration>();

                var currentCountry = configuration[CURRENT_COUNTRY_CONFIG_KEY];

                if (string.IsNullOrEmpty(currentCountry))
                {
                    throw new ConfigurationException("Configuration does not contain country selector");
                }

                var config = new AppConfiguration()
                {
                    ApplicationName = env.ApplicationName,
                    ContentRootPath = env.ContentRootPath,
                    EnvironmentName = env.EnvironmentName,
                    WebRootPath = env.WebRootPath,
                    CurrentMapSelector = currentCountry,
                };

                LoadMaps(config, servicesProvider.GetRequiredService<MapService>());
                LoadLocales(config);

                return config;
            }
        });
    }

    private static void LoadMaps(AppConfiguration config, MapService mapProvider)
    {
        var maps = new List<Map>();

        foreach (var file in Directory.EnumerateFiles(config.MapsFullPath))
        {
            if (file is null || !File.Exists(file))
                continue;

            var buildedMap = mapProvider.GetOrAdd(file);
            maps.Add(buildedMap);

            if (Path.GetFileNameWithoutExtension(file) == config.CurrentMapSelector)
            {
                config.CurrentMap = buildedMap;
            }
        }
    }

    private static void LoadLocales(AppConfiguration config)
    {

    }
}