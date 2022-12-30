using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Web.Infrastructure.Configuration;
using Web.Infrastructure.Models.MapInfo;
using Web.Infrastructure.Models.Repository;
using Web.Infrastructure.Repository;

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
            var map = connection.Query<Map>("Select * from tbl_Maps").FirstOrDefault();

            if (map is null)
            {
                map = AppConfiguration.MapInfo;
                var paramsObject = new DynamicParameters();
                paramsObject.Add("@Caption", map.Caption);
                paramsObject.Add("@Description", map.Description);
                paramsObject.Add("@NEWID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute(RepositoryConstants.Procedurs.INSERT_MAP, paramsObject, commandType: CommandType.StoredProcedure);
                map.Id = paramsObject.Get<int>("@NEWID");

                foreach (var city in map.Cities)
                {
                    var cityParamsObject = new DynamicParameters();
                    cityParamsObject.Add("@MapId", map.Id);
                    cityParamsObject.Add("@Caption", city.Text.Caption);
                    cityParamsObject.Add("@NEWID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(RepositoryConstants.Procedurs.INSERT_CITY, cityParamsObject, commandType: CommandType.StoredProcedure);
                    city.Id = cityParamsObject.Get<int>("@NEWID");
                }
            }
        }
    }
}