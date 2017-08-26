using CorMon.Core.Domain;
using CorMon.Core.Enums;
using CorMon.Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CorMon.Infrastructure.DataProviders
{
    public static class DbContextSeedData
    {
        public static void SeedDatabase(this IServiceScopeFactory scopeFactory)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<IMongoDbContext>();

                var _posts = dbContext.GetCollection<Post>();
                var _users = dbContext.GetCollection<User>();
                if (!_posts.AsQueryable().Any())
                {
                    var user = new User
                    {
                        DisplayName = "hamed shirbandi",
                        Email = "hamed.shirbandi@gmail.com",
                        Phone = "0210000000",
                        UserName = "hamed99",
                        
                    };

                    _users.InsertOne(user);

                    List<Post> posts = new List<Post>();

                    for (int i = 0; i < 10; i++)
                    {
                        var post = new Post
                        {
                            UserId = user.Id,
                            Title = "لورم ایپسوم متن ساختگی با تولید "+i,
                            Content = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
                            CreateDateTime = DateTime.Now,
                            ModifiedDateTime = DateTime.Now,
                            PublishDateTime = DateTime.Now,
                            MetaDescription = "لورم ایپسوم متن ساختگی با تولید سادگی",
                            MetaKeyWords = "ایپسوم,ساختگی,تولید,سادگی",
                            PostLevel = PostLevel.Intro,
                            PostType = PostType.Article,
                            PublishStatus = PublishStatus.Publish,
                            RobotsState = RobotsState.Global,
                            UrlTitle = "لورم-ایپسوم-متن-ساختگی-با-تولید-سادگی",

                        };
                        posts.Add(post);
                    }
                    

                    _posts.InsertMany(posts);
                }

            }
        }
    }
}
