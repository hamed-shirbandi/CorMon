using CorMon.Core.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Infrastructure.DbContext
{
    public interface IMongoDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : BaseEntity;
        void CreateCollection<TEntity>() where TEntity : BaseEntity;
        IList<string> ListCollections();
    }
}
