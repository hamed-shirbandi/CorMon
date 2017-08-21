using CorMon.Core.Domain;
using CorMon.Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


namespace CorMon.Infrastructure.DataProviders
{
    public static class DbContextSeedData
    {
        public static void SeedDatabase(this IServiceScopeFactory scopeFactory)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<IMongoDbContext>();

                var posts = dbContext.GetCollection<Post>();
                var users = dbContext.GetCollection<User>();
                if (!posts.AsQueryable().Any())
                {
                    var user = new User
                    {
                        DisplayName = "hamed shirbandi",
                        Email = "hamed.shirbandi@gmail.com",
                        Phone = "0210000000",
                        UserName = "hamed99"
                    };

                    users.InsertOne(user);


                    var post = new Post
                    {
                        Title="hi core ! hi mongo !",
                        Content="this is CorMon Project !",
                        UserId = user.Id
                    };

                    posts.InsertOne(post);
                }

            }
        }
    }
}
