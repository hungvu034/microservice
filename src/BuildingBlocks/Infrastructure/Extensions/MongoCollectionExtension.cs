using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.SeedWork;
using MongoDB.Driver ; 
namespace Infrastructure.Extensions
{
    public static class MongoCollectionExtension
    {
        public static async Task<PageList<T>> PaginatedListAsync<T>
        (this IMongoCollection<T> colection , FilterDefinition<T> filter , int currentPage , int pageSize)
        where T : class 
        => PageList<T>.PagingList(colection,filter,currentPage,pageSize);
    }
}