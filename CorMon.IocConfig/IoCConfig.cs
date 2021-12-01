using CorMon.Application.Posts;
using CorMon.Core.Data;
using CorMon.Infrastructure.DbContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorMon.IocConfig
{
    public static class IocConfig
    {
        public static void ConfigureIocContainer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton(provider => { return configuration; });

            services.Scan(sc =>
                        sc.FromCallingAssembly()
                          .FromAssemblies(
                            typeof(IPostService).Assembly,
                            typeof(IMongoDbContext).Assembly,
                            typeof(IPostRepository).Assembly
                          )
                        .AddClasses()
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    );
        }

    }
}
