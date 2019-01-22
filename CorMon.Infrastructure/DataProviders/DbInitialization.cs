using CorMon.Core.Domain;
using CorMon.Core.Extensions;
using CorMon.Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using AspNetCore.Identity.Mongo.Model;

namespace CorMon.Infrastructure.DataProviders
{
    public static class DbInitialization
    {
        public static void InitialDatabase(this IServiceScopeFactory scopeFactory)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<IMongoDbContext>();

                CreateCollections(dbContext);

                CreateIndexes(dbContext);
            }
        }



        private static void CreateCollections(IMongoDbContext dbContext)
        {
            var collections = dbContext.ListCollections();

            if (!collections.Has<Post>())
                dbContext.CreateCollection<Post>();

            if (!collections.Has<User>())
                dbContext.CreateCollection<User>();

            if (!collections.Has<Role>())
                dbContext.CreateCollection<Role>();

            if (!collections.Has<Taxonomy>(name: "Taxonomies"))
                dbContext.CreateCollection<Taxonomy>(name: "Taxonomies");
        }




        private static void CreateIndexes(IMongoDbContext dbContext)
        {


            #region Post Indexs

            dbContext.GetCollection<Post>().Indexes.CreateOneAsync(new CreateIndexModel<Post>(Builders<Post>.IndexKeys.Ascending(x => x.Id), new CreateIndexOptions() { Name = "Id", Unique = true }));
            dbContext.GetCollection<Post>().Indexes.CreateOneAsync(new CreateIndexModel<Post>(Builders<Post>.IndexKeys.Ascending(x => x.UserId), new CreateIndexOptions() { Name = "UserId"}));


            #endregion

            #region Taxonomy Indexs

            dbContext.GetCollection<Taxonomy>(name:"Taxonomies").Indexes.CreateOneAsync(new CreateIndexModel<Taxonomy>(Builders<Taxonomy>.IndexKeys.Ascending(x => x.Id), new CreateIndexOptions() { Name = "Id", Unique = true }));
            dbContext.GetCollection<Taxonomy>(name: "Taxonomies").Indexes.CreateOneAsync(new CreateIndexModel<Taxonomy>(Builders<Taxonomy>.IndexKeys.Ascending(x => x.Type), new CreateIndexOptions() { Name = "Type" }));


            #endregion

            #region User Indexs

            dbContext.GetCollection<User>().Indexes.CreateOneAsync(new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(x => x.Id), new CreateIndexOptions() { Name = "Id", Unique = true }));
            dbContext.GetCollection<User>().Indexes.CreateOneAsync(new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(x => x.Email), new CreateIndexOptions() { Name = "Email", Unique = true }));



            #endregion

            #region Role Indexs

            dbContext.GetCollection<Role>().Indexes.CreateOneAsync(new CreateIndexModel<Role>(Builders<Role>.IndexKeys.Ascending(x => x.Id), new CreateIndexOptions() { Name = "Id", Unique = true }));

            #endregion


        }

    }
}
