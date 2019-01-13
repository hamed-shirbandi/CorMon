using CorMon.Application.Mapper;
using CorMon.Application.Posts;
using CorMon.Core.Data;
using CorMon.Core.Domain;
using CorMon.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CorMon.Application.UnitTests.Posts
{
    [TestClass]
    public class PostServiceTests: TestsBase
    {
        #region Fields

        private IPostService postService;


        #endregion

        #region Initialize


        [TestInitialize]
        public void SetupTest()
        {
            // add some fake post & user & taxonomy
            AddFakeData();

            var postRepository = new Mock<IPostRepository>();
            postRepository.Setup(m => m.GetByIdAsync(It.IsAny<string>())).Returns((string id) => Task.FromResult(posts.FirstOrDefault(p => p.Id == id)));

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetAsync(It.IsAny<string>())).Returns((string id) => Task.FromResult(users.FirstOrDefault(p => p.Id == id)));


            var taxonomyRepository = new Mock<ITaxonomyRepository>();
            taxonomyRepository.Setup(m => m.GetListByIds(It.IsAny<string[]>())).Returns((string[] ids) => taxonomies.Where(t => ids.Contains(t.Id)).ToArray());

            //var mapperService = new Mock<IMapperService>();
            //mapperService.Setup(m=>m.BindToOutputModel(It.IsAny<Post>(), It.IsAny<User>(), It.IsAny<IEnumerable<Taxonomy>>(), It.IsAny<IEnumerable<Taxonomy>>())).Returns(GetPostOutputModel());

            var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
            postService = new PostService(postRepository.Object, userRepository.Object, taxonomyRepository.Object, new MapperService(configuration));
        }



        #endregion

        #region Tests



        [TestMethod]
        public async Task Can_Get_Post()
        {
            var fakePost = posts.FirstOrDefault();
            var post =await postService.GetAsync(fakePost.Id);

            Assert.AreEqual(post.Id, fakePost.Id);
        }




        #endregion

        #region Private Methods


        private void AddFakeData()
        {
            AddTaxonomies();
            AddUsers();
            AddFakePosts();
        }



        private void AddUsers()
        {
            users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "UserName",
                    DisplayName = "DisplayName",
                    Email = "test@example.com",
                    Phone = "0911111111",
                    About = "About",
                }
            };
        }

        private void AddTaxonomies()
        {
            taxonomies = new List<Taxonomy>();

            for (int i = 1; i <= 6; i++)
            {
                taxonomies.Add(new Taxonomy
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Name " + i,
                    UrlTitle = "Title_" + i,
                    Description = "Description " + i,
                    Type =  TaxonomyType.Tag,
                    PostCount = 10,
                });

            }
        }




        private void AddFakePosts()
        {
            posts = new List<Post>();

            for (int i = 1; i <= 10; i++)
            {
                posts.Add(new Post
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Title " + i,
                    UrlTitle = "Title_" + i,
                    Content = "Content " + i,
                    Author = "Author " + i,
                    PostLevel = PostLevel.Advance,
                    PublishDateTime = DateTime.Now,
                    PublishStatus = PublishStatus.Publish,
                    UserId = users.FirstOrDefault().Id,
                    TagIds = taxonomies.Where(t => t.Type == TaxonomyType.Tag).Select(t => t.Id).ToArray(),
                    CategoryIds = taxonomies.Where(t => t.Type == TaxonomyType.Category).Select(t => t.Id).ToArray(),
                });

            }

        }



        #endregion
    }
}
