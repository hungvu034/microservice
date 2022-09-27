namespace Shared.SeedWork
{
    public class PagedList<T> : List<T> 
    {
        public PagedList(IEnumerable<T> items , int totalItems , int currentPage , int pageSize)
        {
            _metadata = new MetaData(){
                TotalItems = totalItems , 
                CurrentPage = currentPage , 
                PageSize = pageSize 
            };
            AddRange(items);
        }

        private MetaData _metadata { get; }

        public MetaData GetMetaData(){
            return _metadata ; 
        }

    }
    
}