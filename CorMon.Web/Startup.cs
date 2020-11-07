using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CorMon.Infrastructure.DataProviders;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using CorMon.IocConfig;
using RedisCache.Core;
using Microsoft.Extensions.Configuration;
using AspNetCore.Identity.Mongo;
using CorMon.Core.Domain;
using Microsoft.Extensions.Hosting;

namespace CorMon.Web
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


            services.AddControllersWithViews();
          
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
            else
            {
                app.UseExceptionHandler("/Error/Unknown");
            }


            serviceScopeFactory.InitialDatabase();
            serviceScopeFactory.SeedDatabase();


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
 
        }



    }
}
