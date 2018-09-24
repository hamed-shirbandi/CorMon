using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorMon.Infrastructure.DataProviders;
using CorMon.IocConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedisCache.Core;
using Swashbuckle.AspNetCore.Swagger;

namespace CorMon.Web.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region Caching

            services.AddRedisCache(options =>
            {
                options.Configuration = _configuration["RedisCache:Connection"];
                options.InstanceName = _configuration["RedisCache:InstanceName"];
            });


            #endregion

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CorMon API Documentation", Version = "v1" });
            });

            services.AddMvc();
            return services.ConfigureIocContainer(_configuration);
        }




        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceScopeFactory serviceScopeFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
      

            app.UseStaticFiles();


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });

            serviceScopeFactory.InitialDatabase();
            serviceScopeFactory.SeedDatabase();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=StagingTests}/{action=Get_Api_Documentation}");
            });
        }
    }
}
