using CorMon.Core.Domain;
using CorMon.Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Infrastructure.DataProviders
{
    public static class DbInitialization
    {
        public static void InitialDatabase(this IServiceScopeFactory scopeFactory)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<IMongoDbContext>();
                var collections = dbContext.ListCollections();

                // == dbContext.GetCollection<Post>()
                if (!collections.Contains(typeof(Post).Name.ToLower()+"s"))
                {
                    dbContext.CreateCollection<Post>(); 
                }


                // == dbContext.GetCollection<User>()
                if (!collections.Contains(typeof(User).Name.ToLower() + "s"))
                {
                    dbContext.CreateCollection<User>();
                }

                // == dbContext.GetCollection<Taxonomy>(name: "taxonomies")
                if (!collections.Contains("taxonomies"))
                {
                    dbContext.CreateCollection<Taxonomy>(name: "taxonomies");
                }
            }
        }
    }
}
