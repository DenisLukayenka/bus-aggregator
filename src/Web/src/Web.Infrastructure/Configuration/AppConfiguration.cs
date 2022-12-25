﻿using Web.Infrastructure.Models.Country;

namespace Web.Infrastructure.Configuration;

public class AppConfiguration
{
    public const string CONFIGURATION_PROJECT_NAME = "Web.Configuration";
    public const string MAPS_PATH = "maps";
    public const string LOCALES_PATH = "locales";

    public string WebRootPath { get; set; }
    public string EnvironmentName { get; set; }
    public string ApplicationName { get; set; }
    public string ContentRootPath { get; set; }

    public string CurrentMapSelector { get; set; }
    public Map CurrentMap { get; set; }
    public ICollection<Map> Maps { get; set; }

    public string ConfigsFullPath
    {
        get
        {
            var parentDirectoryInfo = new DirectoryInfo(ContentRootPath).Parent;

            if (parentDirectoryInfo is null)
            {
                throw new DirectoryNotFoundException($"The root directory of the solution was not found, the current is: {ContentRootPath}");
            }

            var configsPath = Path.Combine(parentDirectoryInfo.FullName, CONFIGURATION_PROJECT_NAME);

            return configsPath;
        }
    }

    public string MapsFullPath => Path.Combine(ConfigsFullPath, MAPS_PATH);
    public string LocalesFullPath => Path.Combine(ConfigsFullPath, LOCALES_PATH);
}
