using CorMon.Core.Domain;
using CorMon.Core.Extensions;
using CorMon.Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;


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


                if (!collections.Has<Post>())
                    dbContext.CreateCollection<Post>();

                if (!collections.Has<User>())
                    dbContext.CreateCollection<User>();

                if (!collections.Has<Taxonomy>(name: "taxonomies"))
                    dbContext.CreateCollection<Taxonomy>();

            }
        }
    }
}
