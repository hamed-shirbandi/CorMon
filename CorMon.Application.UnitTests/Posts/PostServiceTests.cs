using CorMon.Application.Posts;
using CorMon.Application.Posts.Dto;
using CorMon.Core.Data;
using CorMon.Core.Domain;
using CorMon.Infrastructure.DbContext;
using CorMon.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CorMon.Application.UnitTests.Posts
{
    [TestClass]
    public class PostServiceTests
    {
        private IPostService postService;

        [TestInitialize]
        public void SetupTest()
        {
        
            //To Do:  mock postService ...
        }



        [TestMethod]
        public void Can_Create_Post()
        {
            var postInput = new PostInput
            {
                Title = "test post",
                UserId = "599b295c03a89924849735b3",
                Content = "test content",
                Author = "admin",
                UrlTitle = "test-post",
                Id = "599b295c03a89924849735b4"
            };

            var result = postService.CreateAsync(postInput).Result;
            var post = postService.Get(result.id);

            Assert.AreEqual(result.id, post.Id);
        }
    }
}
