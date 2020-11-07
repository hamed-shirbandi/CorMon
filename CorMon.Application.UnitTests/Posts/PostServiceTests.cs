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
using MongoDB.Bson;

namespace CorMon.Application.UnitTests.Posts
{
    [TestClass]
    public class PostServiceTests: TestsBase
    {
        #region Fields

        private IPostService postService;


        #endregion

        #region Ctor

        public PostServiceTests():base()
        {

        }

        #endregion

        #region Initialize


        [TestInitialize]
        public void SetupTest()
        {
           
            var postRepository = new Mock<IPostRepository>();
            postRepository.Setup(m => m.GetByIdAsync(It.IsAny<string>())).Returns((string id) => Task.FromResult(posts.FirstOrDefault(p => p.Id == id)));

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetAsync(It.IsAny<string>())).Returns((string id) => Task.FromResult(users.FirstOrDefault(p => p.Id == new ObjectId(id))));


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

        

        #endregion
    }
}
