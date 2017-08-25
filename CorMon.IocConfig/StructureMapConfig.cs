using CorMon.Application.Posts;
using CorMon.Core.Data;
using CorMon.Infrastructure.DbContext;
using CorMon.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StructureMap;
using System;

namespace CorMon.IocConfig
{
   public static class StructureMapConfig
    {
        public static IServiceProvider ConfigureIocContainer(this IServiceCollection services)
        {
            var container = new Container();
            container.Configure(config =>
            {
                config.For<IPostService>().Use<PostService>();
                config.For<IPostRepository>().Use<PostRepository>();
                config.For<IMongoDbContext>().Use<MongoDbContext>();
                config.For<IMongoClient>().Use<MongoClient>();
            });

            container.Populate(services);

            return container.GetInstance<IServiceProvider>();
        }

    }
}
