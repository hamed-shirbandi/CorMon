using CorMon.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using CorMon.Core.Domain;
using System.Threading.Tasks;
using CorMon.Infrastructure.DbContext;
using MongoDB.Driver;
using System.Linq;
using CorMon.Core.Enums;

namespace CorMon.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        #region Fields

        //private readonly IMongoDbContext _dbContext;
        private readonly IMongoCollection<Post> _posts;

        #endregion

        #region Ctor

        public PostRepository(IMongoDbContext dbContext)
        {
            _posts = dbContext.GetCollection<Post>();
        }



        #endregion

        #region Methods



        /// <summary>
        /// 
        /// </summary>
        public async Task<Post> GetAsync(string id)
        {
            return await _posts.Find(e => e.Id == id).FirstOrDefaultAsync();
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<Post> GetAsync(string title, PostType postType)
        {
            return await _posts.Find(p => p.Title == title && p.PostType==postType).FirstOrDefaultAsync();
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<Post> InsertAsync(Post post)
        {
            await _posts.InsertOneAsync(post);
            return post;
        }




        /// <summary>
        /// 
        /// </summary>
        public Task InsertAsync(IEnumerable<Post> posts)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<Post>> SearchAsync(string term)
        {
           return  await _posts.Find(p=>p.Title.Contains(term)).ToListAsync();
           
        }




        /// <summary>
        /// 
        /// </summary>
        public Task<Post> UpdateAsync(Post post)
        {
            throw new NotImplementedException();
        }




        /// <summary>
        /// 
        /// </summary>
        public Task UpdateAsync(IEnumerable<Post> posts)
        {
            throw new NotImplementedException();
        }



        #endregion


    }
}
