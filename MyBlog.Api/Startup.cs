using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBlog.Api.Configs;
using MyBlog.Api.DataAccess.Abstractions;
using MyBlog.Api.DataAccess.Repositories;
using MyBlog.Api.Filters;

namespace MyBlog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            RegisterDevDepdendencies(services);
#else
            RegisterDependencies(services);
#endif

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            //
            // Register configurations
            //
            services.AddSingleton(provider =>
            {
                var connectionString = Configuration["BlogConnectionString"];
                var dbConfig = new DatabaseConfig {ConnectionString = connectionString};

                return dbConfig;
            });
            //
            // Register repositories
            //
            services.AddTransient<IPostsRepository, PostsRepository>();
            //
            // Register filters
            //
            services.AddTransient<IStartupFilter, DatabaseInitFilter>();
        }

        private void RegisterDevDepdendencies(IServiceCollection services)
        {
            //
            // Register configurations
            //
            services.Configure<DatabaseConfig>(Configuration.GetSection("DatabaseConfig"));
            services.AddSingleton(provider =>
            {
                var dbConfig = provider.GetRequiredService<IOptions<DatabaseConfig>>().Value;
                return dbConfig;
            });
            //
            // Register repositories
            //
            services.AddTransient<IPostsRepository, PostsRepository>();
            //
            // Register filters
            //
            services.AddTransient<IStartupFilter, DevDatabaseInitFilter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
