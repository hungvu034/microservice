using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using MongoDB.Driver;
using Shared.Anotations;

namespace Infrastructure.Common
{
    public class MongoDbRepositoryBase<T> : IMongoDbRepositoryBase<T>
    where T : MongoEntity
    {
        private readonly IMongoDatabase _database;

        protected virtual IMongoCollection<T> Collection { get; }
        public MongoDbRepositoryBase(IMongoClient client,Shared.Configurations.MongoDatabaseSettings settings)
        {
            _database = client.GetDatabase(settings.DatabaseName);
            Collection = _database.GetCollection<T>(GetCollectionName());
        }
        public Task CreateAsync(T entity) => Collection.InsertOneAsync(entity);

        public Task DeletedAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            return Collection.DeleteOneAsync(filter);
        }

        public IMongoCollection<T> FindAll(ReadPreference? readPreference = null)
        {
            return _database.GetCollection<T>(GetCollectionName());
        }

        private static string GetCollectionName()
        {
            Type type = typeof(T);
            var mongoDbCollectionAtributte = type.GetCustomAttributes(typeof(MongoDbCollectionAtributte), true).FirstOrDefault()
                                             as MongoDbCollectionAtributte;
            if (mongoDbCollectionAtributte != null)
            {
                return mongoDbCollectionAtributte.CollectionName;
            }
            throw new Exception(type.FullName + " must have MongoDbCollectionAtributte");
        }
        public Task UpdateAsync(T entity)
        {
            Expression<Func<T, string>> func = f => f.Id;
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            return Collection.ReplaceOneAsync(filter, entity);
        }
    }
}