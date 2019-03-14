using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace MyBlog.Integration.Tests
{
    public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddEnvironmentVariables(); 
            });

            base.ConfigureWebHost(builder);

            var env = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);
            if (!string.IsNullOrEmpty(env))
            {
                builder.UseEnvironment(env);
            }
        }
    }
}