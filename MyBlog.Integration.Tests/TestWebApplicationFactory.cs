using System;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;

namespace MyBlog.Integration.Tests
{
    public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var currentEnvironment = builder.GetSetting("environment");
            if (!string.IsNullOrEmpty(currentEnvironment))
            {
                builder.UseEnvironment(currentEnvironment);
            }

            return base.CreateServer(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            //var env = Environment.GetEnvironmentVariable("environment");
            //if (!string.IsNullOrEmpty(env))
            //{
            //    builder.UseEnvironment(env);
            //}
        }
    }
}