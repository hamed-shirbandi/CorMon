using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Mongo;
using CorMon.Core.Domain;
using CorMon.Infrastructure.DataProviders;
using CorMon.IocConfig;
using CorMon.Web.Api.Services.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
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

            #region Identity

            services.AddIdentityMongoDbProvider<User, Role>(identityOptions =>
            {
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireDigit = false;
            }, mongoIdentityOptions => {
                mongoIdentityOptions.ConnectionString = _configuration["Mongo:Connection"] + "/" + _configuration["Mongo:Database"];
            });

            #endregion

            services.AddJwt(options =>
            {
                _configuration.GetSection("Jwt").Bind(options);

            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_configuration["Swagger:Version"], new OpenApiInfo { Title = _configuration["Swagger:Title"], Version = _configuration["Swagger:Version"] });
                var includeXmlComments = _configuration["Swagger:IncludeXmlComments"].Split(",");
                foreach (var includeXmlComment in includeXmlComments)
                    c.IncludeXmlComments(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, includeXmlComment));
            });
          

            services.AddControllers();
            return services.ConfigureIocContainer(_configuration);
        }




        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceScopeFactory serviceScopeFactory)
        {
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });


       

            serviceScopeFactory.InitialDatabase();
            serviceScopeFactory.SeedDatabase();


            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Help}/{action=Get_Api_Documentation_Url}/{id?}");
            });
        }
    }
}
