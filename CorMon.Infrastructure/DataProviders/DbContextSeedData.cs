using CorMon.Core.Domain;
using CorMon.Core.Enums;
using CorMon.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CorMon.Infrastructure.DataProviders
{
    public static class DbContextSeedData
    {

        public static async void SeedDatabase(this IServiceScopeFactory scopeFactory)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                string adminRoleId = string.Empty;
                var dbContext = serviceScope.ServiceProvider.GetService<IMongoDbContext>();
                var configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                var _posts = dbContext.GetCollection<Post>();
                var _users = dbContext.GetCollection<User>();
                var _taxonomies = dbContext.GetCollection<Taxonomy>(name: "taxonomies");

                #region Admin User Role


                var roleName = "Admin";

                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new Role { Name = roleName });
                }


                var user = new User
                {
                    UserName = configuration["Identity:SuperUser:Username"],
                    Email = configuration["Identity:SuperUser:Email"],
                    PhoneNumber = configuration["Identity:SuperUser:PhoneNumber"],
                    DisplayName = "مدیر اصلی سیستم",
                };

                string password = configuration["Identity:SuperUser:Password"];

                if (await userManager.FindByNameAsync(user.UserName) == null)
                {
                    var createSuperUser = await userManager.CreateAsync(user, password);

                    if (createSuperUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }

                }


                #endregion

                #region Taxonomies

                if (!_taxonomies.AsQueryable().Any())
                {
                    List<Taxonomy> taxonomies = new List<Taxonomy>();

                    for (int i = 0; i < 5; i++)
                    {

                        var cat = new Taxonomy
                        {
                            Name = "دسته بندی شماره " + i,
                            Description = "توضیح تست برای دسته بندی",
                            PostCount = 0,
                            Type = TaxonomyType.Category,
                            UrlTitle = "دسته-بندی-شماره-" + i,
                        };
                        taxonomies.Add(cat);


                        var tag = new Taxonomy
                        {
                            Name = "برچسب شماره " + i,
                            Description = "توضیح تست برای برچسب",
                            PostCount = 0,
                            Type = TaxonomyType.Tag,
                            UrlTitle = "برچسب-شماره-" + i,
                        };
                        taxonomies.Add(tag);

                    }

                    _taxonomies.InsertMany(taxonomies);

                }



                #endregion

                #region Posts


                if (!_posts.AsQueryable().Any())
                {


                    List<Post> posts = new List<Post>();
                     user = _users.AsQueryable().FirstOrDefault();
                    var taxonomies = _taxonomies.Find(t => true).ToList();
                    var tagIds = taxonomies.Where(t => t.Type == TaxonomyType.Tag).Select(t => t.Id).ToArray();
                    var categoryIds = taxonomies.Where(t => t.Type == TaxonomyType.Category).Select(t => t.Id).ToArray();

                    for (int i = 0; i < 10; i++)
                    {
                        var post = new Post
                        {
                            UserId = user.Id,
                            Author = user.DisplayName,
                            Title = "لورم ایپسوم متن ساختگی با تولید " + i,
                            Content = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
                            CreateDateTime = DateTime.Now,
                            ModifiedDateTime = DateTime.Now,
                            PublishDateTime = DateTime.Now,
                            MetaDescription = "لورم ایپسوم متن ساختگی با تولید سادگی",
                            MetaKeyWords = "ایپسوم,ساختگی,تولید,سادگی",
                            PostLevel = PostLevel.Intro,
                            PublishStatus = PublishStatus.Publish,
                            MetaRobots = RobotsState.Global,
                            UrlTitle = "لورم-ایپسوم-متن-ساختگی-با-تولید-سادگی-" + i,
                            TagIds = tagIds,
                            CategoryIds = categoryIds,

                        };
                        posts.Add(post);
                    }


                    _posts.InsertMany(posts);

                    //taxonomies inc PostCount
                    var filter = Builders<Taxonomy>.Filter.Empty;
                    var update = Builders<Taxonomy>.Update.Inc(t => t.PostCount, 10);
                    var result = _taxonomies.UpdateMany(filter, update);

                }



                #endregion


            }
        }
    }
}
