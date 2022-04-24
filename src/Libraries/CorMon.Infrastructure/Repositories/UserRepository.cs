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
using MongoDB.Bson;

namespace CorMon.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Fields

        private readonly IMongoCollection<User> _users;

        #endregion

        #region Ctor

        public UserRepository(IMongoDbContext dbContext)
        {
            _users = dbContext.GetCollection<User>();
        }



        #endregion

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        public async Task<User> GetAsync(string id)
        {
            return await _users.Find(e => e.Id == new ObjectId(id)).FirstOrDefaultAsync();
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _users.Find(e => e.Email == email).FirstOrDefaultAsync();
        }

      


        /// <summary>
        /// 
        /// </summary>
        public async Task<User> GetFirstUserAsync()
        {
            return await _users.AsQueryable().FirstOrDefaultAsync();
        }


        /// <summary>
        /// 
        /// </summary>
        public User Get(string id)
        {
            return  _users.Find(e => e.Id == new ObjectId(id)).FirstOrDefault();
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task UpdateAsync(User user)
        {
            await _users.ReplaceOneAsync(x => x.Id == user.Id, user, new UpdateOptions() { IsUpsert = false });

        }





        #endregion


    }
}
