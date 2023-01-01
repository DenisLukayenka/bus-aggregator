using Web.Infrastructure.Exceptions;
using Web.Infrastructure.Models.MapInfo;

namespace Web.Infrastructure.Configuration;

public static class AppConfigurationExtensions
{
    public static void MergeMapsIdentifiers(this AppConfiguration target, Map source)
    {
        target.MapInfo.Id = source.Id;
    }

    public static void MergeCitiesIdentifiers(this AppConfiguration target, ICollection<City> source)
    {
        foreach (var city in target.MapInfo.Cities)
        {
            var sourceCity = source.FirstOrDefault(c => string.Compare(c.Text.Caption, city.Text.Caption, true) == 0);

            if (sourceCity is null)
            {
                throw new ConfigurationException("Cannot merge db instance with configuration instance. Cannot find city with caption: " + city.Text.Caption);
            }

            city.Id = sourceCity.Id;
        }
    }
}