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
    public class TaxonomyRepository : ITaxonomyRepository
    {
        #region Fields

        private readonly IMongoCollection<Taxonomy> _taxonomies;

        #endregion

        #region Ctor

        public TaxonomyRepository(IMongoDbContext dbContext)
        {
            _taxonomies = dbContext.GetCollection<Taxonomy>(name:"taxonomies");
        }



        #endregion

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        public async Task<Taxonomy> GetByIdAsync(string id)
        {
            return await _taxonomies.Find(t => t.Id == id).FirstOrDefaultAsync();
        }



        /// <summary>
        /// 
        /// </summary>
        public  Taxonomy GetById(string id)
        {
            return  _taxonomies.Find(t => t.Id == id).FirstOrDefault();
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<Taxonomy> GetByNameAsync(string name)
        {
            return await _taxonomies.Find(t => t.Name == name).FirstOrDefaultAsync();
        }





        /// <summary>
        /// 
        /// </summary>
        public async Task CreateAsync(Taxonomy tax)
        {
          await _taxonomies.InsertOneAsync(tax);
        
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task CreateAsync(IEnumerable<Taxonomy> tax)
        {
         
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<Taxonomy>> SearchAsync(string term, TaxonomyType? type, SortOrder sortOrder)
        {
            var queryable = _taxonomies.AsQueryable();

            #region براساس متن

            if (!string.IsNullOrEmpty(term))
            {
                queryable = queryable.Where(t => t.Name.Contains(term));
            }

            #endregion

            #region براساس نوع

            if (type.HasValue)
            {
                queryable = queryable.Where(t => t.Type == type);
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
        public async Task UpdateAsync(Taxonomy tax)
        {
          await _taxonomies.ReplaceOneAsync(t => t.Id == tax.Id, tax, new UpdateOptions() { IsUpsert = false });
        }




        /// <summary>
        /// 
        /// </summary>
        public Task UpdateAsync(IEnumerable<Taxonomy> taxs)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<Taxonomy>> GetListByIdsAsync(string[] taxIds)
        {
            return await _taxonomies.Find(t => taxIds.Contains(t.Id)).ToListAsync();
        }




        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Taxonomy> GetListByIds(string[] taxIds)
        {
            return _taxonomies.Find(t => taxIds.Contains(t.Id)).ToList(); 
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<Taxonomy>> GetAllAsync(TaxonomyType type)
        {
            return await _taxonomies.Find(t => t.Type == type).ToListAsync();
        }



        #endregion


    }
}
