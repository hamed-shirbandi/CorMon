using CorMon.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using CorMon.Core.Domain;
using System.Threading.Tasks;
using CorMon.Infrastructure.DbContext;
using MongoDB.Driver;
using CorMon.Core.Enums;
using MongoDB.Driver.Linq;

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
        public async Task<Post> GetByIdAsync(string id)
        {
            return await _posts.Find(e => e.Id == id).FirstOrDefaultAsync();
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<Post> GetByTitleAsync(string title)
        {
            return await _posts.Find(p => p.Title == title).FirstOrDefaultAsync();
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
        public async Task<IEnumerable<Post>> SearchAsync(string term, PublishStatus? publishStatus, SortOrder sortOrder)
        {
            var queryable = _posts.AsQueryable();

            #region براساس متن

            if (!string.IsNullOrEmpty(term))
            {
                queryable = queryable.Where(p => p.Title.Contains(term));
            }

            #endregion

            #region براساس وضعیت انتشار

            if (publishStatus.HasValue)
            {
                queryable = queryable.Where(p => p.PublishStatus == publishStatus);
            }

            #endregion


            #region مرتب سازی

            queryable = sortOrder == SortOrder.Asc ? queryable.OrderBy(p => p.Id) : queryable.OrderByDescending(p => p.Id);


            #endregion


            return await queryable.ToListAsync();
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
