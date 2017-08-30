using CorMon.Application.Posts;
using CorMon.Application.Taxonomies;
using CorMon.Application.Users;
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
                config.For<IUserService>().Use<UserService>();
                config.For<IUserRepository>().Use<UserRepository>();
                config.For<ITaxonomyRepository>().Use<TaxonomyRepository>();
                config.For<ITaxonomyService>().Use<TaxonomyService>();
                
            });
            

            container.Populate(services);

            return container.GetInstance<IServiceProvider>();
        }

    }
}
