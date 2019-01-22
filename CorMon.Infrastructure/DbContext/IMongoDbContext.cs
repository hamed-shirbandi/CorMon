using CorMon.Core.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Infrastructure.DbContext
{
    public interface IMongoDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name = "");
        void CreateCollection<TEntity>(string name = "");
        IList<string> ListCollections();
    }
}
