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
           
            Assert.AreEqual(1, 1);
        }
    }
}
