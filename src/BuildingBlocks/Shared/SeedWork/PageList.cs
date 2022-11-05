using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
namespace Shared.SeedWork
{
    public class PageList<T> : List<T>
    {
        public PageList(IEnumerable<T> items, int currentPage, int pageSize, int totalItems)
        {
            _metaData = new MetaData()
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = totalItems
            };
            AddRange(items);
        }
        private MetaData _metaData;
        public MetaData MetaData
        {
            get => _metaData;
        }
        public static PageList<T> PagingList(IEnumerable<T> items , Func<T,bool> condition , int pageIndex, int pageSize)
        {
            IEnumerable<T> pagingItems = items.Where(condition)
                                        .Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize);
            PageList<T> result = new PageList<T>(pagingItems, pageIndex, pageSize, items.Count());
            return result;
        }
        public static PageList<T> PagingList(IMongoCollection<T> collection, FilterDefinition<T> filter, int pageIndex, int pageSize)
        {
            long totalItems = collection.Find(filter).CountDocuments();
            IEnumerable<T> pagingItems = collection.Find(filter)
                                                   .Skip((pageIndex - 1) * pageSize)
                                                   .Limit(pageSize).ToEnumerable();
            PageList<T> result = new(pagingItems , pageIndex , pageSize , (int)totalItems);
            return result ;   
        }
    }
}