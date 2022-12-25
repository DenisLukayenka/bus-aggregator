using Microsoft.AspNetCore.Builder;
using Web.Infrastructure.Configuration;
using Web.Infrastructure.Exceptions;

namespace Web.SqlMigration;

public static class SqlDatabaseApplicationBuilderExtensions
{
    public static void UseSqlMaps(this IApplicationBuilder app)
    {
        if (app.ApplicationServices.GetService(typeof(AppConfiguration)) is not AppConfiguration config)
            throw new ConfigurationException("Cannot initialize configuration instance");

        
    }
}