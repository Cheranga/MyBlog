using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MyBlog.Api;

namespace MyBlog.Integration.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrEmpty(environmentName))
            {
                builder.UseEnvironment(environmentName);
            }
            //if (string.IsNullOrEmpty(environmentName))
            //{
            //    throw new ArgumentException($"{nameof(TestWebApplicationFactory)}.{nameof(ConfigureWebHost)} needs environment variable ASPNETCORE_ENVIRONMENT to set environment.");
            //}

            //builder.UseEnvironment(environmentName);
        }
    }
}