using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Domains;
using MongoDB.Driver;

namespace Contracts.Common.Interfaces
{
    public interface IMongoDbRepositoryBase<T> where T : MongoEntity
    {
        IMongoCollection<T> FindAll(ReadPreference? readPreference = null);

        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeletedAsync(T entity);
    }
}