using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CorMon.UITests.Base
{
    public class TestsBase
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public TestsBase()
        {
            ServiceProvider = GetServiceProvider();
        }

        private IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            Configuration = new ConfigurationBuilder()
                                            .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                                            .Build();

            services.AddSingleton<IConfiguration>(provider => Configuration);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }




        /// <summary>
        /// Creates an IServiceScope which contains an IServiceProvider used to resolve dependencies from a newly created scope and then runs an associated callback.
        /// </summary>
        protected void RunScopedService<T, S>(IServiceProvider serviceProvider, Action<S, T> callback)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<S>();

                callback(context, serviceScope.ServiceProvider.GetRequiredService<T>());
                if (context is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        /// <summary>
        /// Creates an IServiceScope which contains an IServiceProvider used to resolve dependencies from a newly created scope and then runs an associated callback.
        /// </summary>
        protected void RunScopedService<S>(IServiceProvider serviceProvider, Action<S> callback)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<S>();
                callback(context);
                if (context is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        /// <summary>
        /// Creates an IServiceScope which contains an IServiceProvider used to resolve dependencies from a newly created scope and then runs an associated callback.
        /// </summary>
        protected T RunScopedService<T, S>(IServiceProvider serviceProvider, Func<S, T> callback)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<S>();
                return callback(context);
            }
        }
    }
}