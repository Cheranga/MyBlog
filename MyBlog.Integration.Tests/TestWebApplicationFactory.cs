using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using MyBlog.Api;

namespace MyBlog.Integration.Tests
{
    public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            builder.ConfigureTestContainer()
            const string env = "ASPNETCORE_ENVIRONMENT";
            var currentEnvironment = Environment.GetEnvironmentVariable(env);
            var environment = builder.GetSetting(currentEnvironment);
            builder.UseEnvironment(environment);

            currentEnvironment = Environment.GetEnvironmentVariable(env);

            var testServer = base.CreateServer(builder);

            currentEnvironment = Environment.GetEnvironmentVariable(env);

            return testServer;
        }

        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    base.ConfigureWebHost(builder);
        //    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //    if (!string.IsNullOrEmpty(environmentName))
        //    {
        //        builder.UseEnvironment(environmentName);
        //    }
        //    //if (string.IsNullOrEmpty(environmentName))
        //    //{
        //    //    throw new ArgumentException($"{nameof(TestWebApplicationFactory)}.{nameof(ConfigureWebHost)} needs environment variable ASPNETCORE_ENVIRONMENT to set environment.");
        //    //}

        //    //builder.UseEnvironment(environmentName);
        //}
    }
}