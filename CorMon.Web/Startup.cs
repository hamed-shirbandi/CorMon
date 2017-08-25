using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CorMon.Infrastructure.DataProviders;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using CorMon.IocConfig;

namespace CorMon.Web
{
    public class Startup
    {

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            return services.ConfigureIocContainer();
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
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            

            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("fa-IR")),
                SupportedCultures = new[]
                           {
                    new CultureInfo("en-US"),
                    new CultureInfo("fa-IR")
                },
                SupportedUICultures = new[]
                           {
                    new CultureInfo("en-US"),
                    new CultureInfo("fa-IR")
                }
            };
            requestLocalizationOptions.RequestCultureProviders.RemoveAt(2);
            app.UseRequestLocalization(requestLocalizationOptions);

            app.UseStaticFiles();

            serviceScopeFactory.InitialDatabase();
            serviceScopeFactory.SeedDatabase();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }



    }
}
