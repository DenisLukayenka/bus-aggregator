using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Configuration;
using Web.Infrastructure.Exceptions;
using Web.Infrastructure.Models.MapInfo;

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
                    MapSelector = currentCountry,
                };

                LoadMap(config);
                LoadLocales(config);

                return config;
            }
        });
    }

    private static void LoadMap(AppConfiguration config)
    {
        var targetMap = Directory
            .EnumerateFiles(config.MapsFolderFullPath)
            .FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == config.MapSelector);

        if (targetMap is null)
        {
            throw new ConfigurationException("Cannot find map configuration file with name: " + config.MapSelector);
        }

        var serializer = new XmlSerializer(typeof(Map), "http://schemas.datacontract.org/2004/07/Web.Infrastructure.Models.MapInfo");

        using (var fs = File.Open(targetMap, FileMode.Open))
        {
            if (serializer.Deserialize(fs) is not Map map)
            {
                throw new ConfigurationException("Cannot deserialize map file from: " + targetMap);
            }

            config.MapInfo = map;
        }
    }

    private static void LoadLocales(AppConfiguration config)
    {

    }
}