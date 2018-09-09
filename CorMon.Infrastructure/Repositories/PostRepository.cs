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
using System.Linq;

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

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        public async Task<Post> GetByIdAsync(string id)
        {
            return await _posts.Find(p => p.Id == id).FirstOrDefaultAsync();
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
        public async Task CreateAsync(Post post)
        {
            await _posts.InsertOneAsync(post);

        }




        /// <summary>
        /// 
        /// </summary>
        public async Task CreateAsync(IEnumerable<Post> posts)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Post> Search(int page, int recordsPerPage, string term, bool isTrashed, PublishStatus? publishStatus, SortOrder sortOrder, out int pageSize, out int TotalItemCount)
        {
            var queryable = _posts.AsQueryable();

            #region By isTrashed

            queryable = queryable.Where(p=>p.IsTrashed== isTrashed);

            #endregion
            
            #region By term

            if (!string.IsNullOrEmpty(term))
            {
                queryable = queryable.Where(p => p.Title.Contains(term));
            }

            #endregion

            #region By publishStatus

            if (publishStatus.HasValue)
            {
                queryable = queryable.Where(p => p.PublishStatus == publishStatus);
            }

            #endregion

            #region SortOrder

            queryable = sortOrder == SortOrder.Asc ? queryable.OrderBy(p => p.Id) : queryable.OrderByDescending(p => p.Id);


            #endregion

            #region  Skip Take

            TotalItemCount = queryable.CountAsync().Result;
            pageSize = (int)Math.Ceiling((double)TotalItemCount / recordsPerPage);

            page = page > pageSize || page < 1 ? 1 : page;

          
            var skiped = (page - 1) * recordsPerPage;
            queryable = queryable.Skip(skiped).Take(recordsPerPage);


            #endregion
            

            return queryable.ToList();
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<Post>> SearchAsync(int page, int recordsPerPage, string term, string taxonomyId , TaxonomyType? taxonomyType , PublishStatus? publishStatus, SortOrder sortOrder)
        {
            var queryable = _posts.AsQueryable();

            #region By IsTrashed

            queryable = queryable.Where(p => p.IsTrashed == false);

            #endregion

            #region By term

            if (!string.IsNullOrEmpty(term))
            {
                queryable = queryable.Where(p => p.Title.Contains(term));
            }

            #endregion

            #region By publishStatus

            if (publishStatus.HasValue)
            {
                queryable = queryable.Where(p => p.PublishStatus == publishStatus);
            }

            #endregion

            #region By taxonomyId & taxonomyType

            if (taxonomyType.HasValue)
            {
                switch (taxonomyType.Value)
                {
                    case TaxonomyType.Category:
                        queryable = queryable.Where(p => p.CategoryIds.Contains(taxonomyId));

                        break;
                    case TaxonomyType.Tag:
                        queryable = queryable.Where(p => p.TagIds.Contains(taxonomyId));
                        break;
                }
            }

            #endregion

            #region By publishStatus

            if (publishStatus.HasValue)
            {
                queryable = queryable.Where(p => p.PublishStatus == publishStatus);
            }

            #endregion

            #region By publishStatus

            if (publishStatus.HasValue)
            {
                queryable = queryable.Where(p => p.PublishStatus == publishStatus);
            }

            #endregion

            #region SortOrder

            queryable = sortOrder == SortOrder.Asc ? queryable.OrderBy(p => p.Id) : queryable.OrderByDescending(p => p.Id);


            #endregion

            #region Skip Take

            var skiped = page * recordsPerPage;


            queryable = queryable.Skip(skiped).Take(recordsPerPage);


            #endregion

            return await queryable.ToListAsync();
        }





        /// <summary>
        /// 
        /// </summary>
        public async Task UpdateAsync(Post post)
        {
            await _posts.ReplaceOneAsync(p => p.Id == post.Id, post, new UpdateOptions() { IsUpsert = false });
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task UpdateAsync(IEnumerable<Post> posts)
        {
            throw new NotImplementedException();
        }





        /// <summary>
        /// 
        /// </summary>
        public async Task DeleteAsync(string id)
        {
            await _posts.DeleteOneAsync(p=>p.Id==id);
        }




        #endregion


    }
}
