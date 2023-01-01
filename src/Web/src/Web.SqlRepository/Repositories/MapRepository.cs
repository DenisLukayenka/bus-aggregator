using System.Data.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using Web.Infrastructure.Configuration;
using Web.Infrastructure.Models.MapInfo;
using Web.Infrastructure.Repository;
using Web.SqlRepository.DAOModels;

namespace Web.SqlRepository.Repositories;

public class MapRepository : BaseRepository<Map>
{
    public MapRepository(AppConfiguration config)
        : base(config)
    {
    }

    public override void Init()
    {
        using (var connection = new SqlConnection(AppConfiguration.ConnectionString))
        {
            if (!TryMergeConfigurationWithDb(connection))
            {
                InsertInDbLastConfiguration(connection);
            }
        }
    }

    private bool TryMergeConfigurationWithDb(DbConnection connection)
    {
        using (var multi = connection.QueryMultiple(SqlQueries.Map.SELECT_WITH_CITIES))
        {
            var dbMap = multi.Read<MapDAO>().SingleOrDefault();
            var dbCities = multi.Read<CityDAO>().ToList();

            var mappedMap = MapDAOModel(dbMap, dbCities);

            if (mappedMap != null)
            {
                AppConfiguration.MergeMapsIdentifiers(mappedMap);
                AppConfiguration.MergeCitiesIdentifiers(mappedMap.Cities);

                return true;
            }

            return false;
        }
    }

    private static Map? MapDAOModel(MapDAO? dbMap, ICollection<CityDAO> dbCities)
    {
        if (dbMap is null || dbCities is null || dbCities.Count == 0)
            return null;


        var cities = dbCities
            .Where(c => c.MapId == dbMap.Id)
            .Select(dbCity => MapDAOModel(dbCity))
            .ToList();

        var map = new Map
        {
            Id = dbMap.Id,
            Caption = dbMap.Caption,
            Description = dbMap.Description,
            Cities = cities,
        };

        return map;
    }

    private static City MapDAOModel(CityDAO dbCity)
    {
        var city = new City
        {
            Id = dbCity.Id,
            Text = new CityText
            {
                Caption = dbCity.Caption
            }
        };

        return city;
    }

    private void InsertInDbLastConfiguration(DbConnection connection)
    {
        var map = AppConfiguration.MapInfo;

        var mapId = connection.QuerySingle<int>(SqlQueries.Map.INSERT, new { map.Caption, map.Description });
        map.Id = mapId;

        foreach (var city in map.Cities)
        {
            var cityId = connection.QuerySingle<int>(SqlQueries.City.INSERT, new { @MapId = map.Id, city.Text.Caption });
            city.Id = cityId;
        }
    }
}