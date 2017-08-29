using CorMon.Core.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Infrastructure.DbContext
{
    public interface IMongoDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name="") where TEntity : BaseEntity;
        void CreateCollection<TEntity>(string name = "") where TEntity : BaseEntity;
        IList<string> ListCollections();
    }
}
