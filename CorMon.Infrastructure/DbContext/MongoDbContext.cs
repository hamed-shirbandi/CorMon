using System;
using System.Collections.Generic;
using System.Text;
using CorMon.Core.Domain;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace CorMon.Infrastructure.DbContext
{
    public class MongoDbContext : IMongoDbContext
    {
        #region Fields

        private readonly string _dbName;
        private readonly string _connectionString;
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _client;

        #endregion

        #region Ctor


        public MongoDbContext(IConfiguration configuration)
        {
            _dbName = configuration["database"];
            _connectionString = configuration["mongoconnection"];
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_connectionString));
            _client = new MongoClient(settings);
            _database = _client.GetDatabase(_dbName);
        }



        #endregion

        #region Methods





        /// <summary>
        /// 
        /// </summary>
        public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : BaseEntity
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower() + "s");
        }





        /// <summary>
        /// 
        /// </summary>
        public void CreateCollection<TEntity>() where TEntity : BaseEntity
        {
             _database.CreateCollection(typeof(TEntity).Name.ToLower() + "s");
        }




        /// <summary>
        /// 
        /// </summary>
        public IList<string> ListCollections() 
        {
           var collections= _database.ListCollections().ToList();
           return  collections.Select(c => c["name"].ToString()).ToList();
        }



        #endregion

    }
}
