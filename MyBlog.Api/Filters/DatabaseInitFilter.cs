using System;
using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MyBlog.Api.Configs;

namespace MyBlog.Api.Filters
{
    public class DatabaseInitFilter : IStartupFilter
    {
        private readonly string _connectionString;

        public DatabaseInitFilter(DatabaseConfig config)
        {
            _connectionString = config?.ConnectionString;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentNullException(nameof(config));
            }

            //if (configuration == null)
            //{
            //    throw new ArgumentNullException(nameof(configuration));
            //}
            ////
            //// TODO: Get the connection string from a strongly typed object
            ////
            //_connectionString = configuration["BlogConnectionString"];

            //if (string.IsNullOrWhiteSpace(_connectionString))
            //{
            //    throw new Exception("Invalid connection string");
            //}
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            EnsureDatabase.For.SqlDatabase(_connectionString);

            var dbUpgradeEngineBuilder = DeployChanges.To
                .SqlDatabase(_connectionString)
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