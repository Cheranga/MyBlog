using System;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;

namespace MyBlog.Integration.Tests
{
    public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        private string _environment = "Development";

        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        protected override IWebHostBuilder CreateWebHostBuilder()
        {   
            var applicationEnvironment = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);
            if (!string.IsNullOrEmpty(applicationEnvironment))
            {
                _environment = applicationEnvironment;
                Environment.SetEnvironmentVariable(AspNetCoreEnvironment, _environment);
            }

            return base.CreateWebHostBuilder();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //var bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            //var field = builder.GetType().GetField("_hostingEnvironment", bindFlags);
            //var hostingEnvironment = field.GetValue(builder) as HostingEnvironment;
            //var environment = hostingEnvironment?.EnvironmentName;

            //if (!string.IsNullOrEmpty(environment))
            //{
            //    builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            //    {
            //        Environment.SetEnvironmentVariable(AspNetCoreEnvironment, environment);
            //        //Environment.SetEnvironmentVariable(AspNetCoreEnvironment, _environment);
            //        //_environment = context.HostingEnvironment.EnvironmentName;
            //    });
            //}

            //builder.ConfigureAppConfiguration(configurationBuilder =>
            //{
            //    configurationBuilder.AddEnvironmentVariables(); 
            //});

            base.ConfigureWebHost(builder);

            var env = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);
            if (!string.IsNullOrEmpty(env))
            {
                builder.UseEnvironment(env);
            }
        }
    }
}