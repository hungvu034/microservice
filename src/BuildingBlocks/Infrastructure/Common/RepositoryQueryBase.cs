using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public class RepositoryQueryBase<T , K ,TContext> : IRepositoryQueryBase<T , K , TContext>
    where T : EntityBase<K> 
    where TContext : DbContext
    { 
        private TContext _context ;

        public RepositoryQueryBase(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
        
        public IQueryable<T> FindAll(bool trackChanges, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            includeProperties.Aggregate(items , (cur , func) => cur.Include(func));
            return items ; 
        }


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            FindAll(trackChanges).Where(expression);
        

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties) =>
            FindAll(trackChanges , includeProperties).Where(expression);

       
        public Task<T> GetByIdAsync(K id) =>
            FindByCondition(x => id.Equals(x.Id)).FirstOrDefaultAsync();
        

        public Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) => 
            FindByCondition(x => id.Equals(x.Id ) , false , includeProperties).FirstOrDefaultAsync();

    }
}