using CorMon.Core.Domain;
using CorMon.Core.Enums;
using CorMon.UnitTests.Base;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorMon.Application.UnitTests
{
   public class TestsBase : UnitTestsBase
    {
        #region Fields

        protected IList<Post> posts;
        protected IList<User> users;
        protected IList<Taxonomy> taxonomies;


        #endregion

        #region Ctor

        public TestsBase()
        {
            AddFakeData();
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
            users = new List<User>();

            for (int i = 1; i <= 6; i++)
            {
                users.Add(new User
                {
                    Id = ObjectId.GenerateNewId(),
                    UserName = "UserName"+ i,
                    DisplayName = "DisplayName" + i,
                    Email = "test@example.com" + i,
                    PhoneNumber = "091111111" + i,
                    About = "About" + i,
                });

            }
            
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
                    Type =(i/2)==0? TaxonomyType.Tag: TaxonomyType.Category,
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
                    UserId = users.FirstOrDefault().Id.ToString(),
                    TagIds = taxonomies.Where(t => t.Type == TaxonomyType.Tag).Select(t => t.Id).ToArray(),
                    CategoryIds = taxonomies.Where(t => t.Type == TaxonomyType.Category).Select(t => t.Id).ToArray(),
                });

            }

        }



        #endregion

    }
}
