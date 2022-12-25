using DbUp;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web.SqlMigration
{
    public static class HostExtensions
    {
        public static IHost SetupDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var servicesProvider = scope.ServiceProvider;
                var configuration = servicesProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Default");

                EnsureDatabase.For.SqlDatabase(connectionString);

                var migrator = DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .WithTransaction()
                    .LogToConsole()
                    .Build();

                var result = migrator.PerformUpgrade();

                return host;
            }
        }
    }
}