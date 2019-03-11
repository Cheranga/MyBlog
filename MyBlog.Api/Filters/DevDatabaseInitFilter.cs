using System;
using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace MyBlog.Api.Filters
{
    public class DevDatabaseInitFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            //
            // TODO: Get this from the configuration depending on the "environment"
            //
            const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogDb;Integrated Security=True;Connect Timeout=30;";

            EnsureDatabase.For.SqlDatabase(connectionString);

            var dbUpgradeEngineBuilder = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(DatabaseInitFilter).Assembly)
                .WithTransaction()
                .LogToAutodetectedLog();

            var dbUpgradeEngine = dbUpgradeEngineBuilder.Build();

            if (dbUpgradeEngine.IsUpgradeRequired())
            {
                var upgradeOperation = dbUpgradeEngine.PerformUpgrade();
                if (!upgradeOperation.Successful)
                {
                    throw new Exception("Database upgrade unsuccessful", upgradeOperation.Error);
                }
            }

            return next;
        }
    }
}