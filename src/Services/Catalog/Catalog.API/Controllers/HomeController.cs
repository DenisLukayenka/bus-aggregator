using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.API.Models;

namespace Services.Catalog.API.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    private readonly string _connectionString;

    public HomeController(IConfiguration configuration)
    {
        this._connectionString = configuration.GetConnectionString("Default");
    }

    [Route("/connection")]
    public string GetConnection()
    {
        return _connectionString;
    }

    [Route("/sql")]
    public string GetSqlVersion()
    {
        using (var con = new SqlConnection(_connectionString))
        {
            con.Open();

            var version = con.ExecuteScalar<string>("SELECT @@VERSION");

            return version;
        }
    }

    [Route("/sqlcities")]
    public IActionResult GetSqlCitiesAsync()
    {
        using (var con = new SqlConnection(_connectionString))
        {
            con.Open();

            var data = con.Query<City>("[dbo].[city_getAll]", commandType: CommandType.StoredProcedure).ToList();

            return new JsonResult(data);
        }
    }
}
