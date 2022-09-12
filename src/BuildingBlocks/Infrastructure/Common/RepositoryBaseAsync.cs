using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common
{
    public class RepositoryBaseAsync<T, K, TContext> : IRepositoryBaseAsync<T, K, TContext>
    where T : EntityBase<K> 
    where TContext : DbContext 
    {
        private TContext _context ; 
        private IUnitofWork<TContext> _unitOfWork ; 
        public RepositoryBaseAsync(TContext context, IUnitofWork<TContext> unitOfWork)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)) ;
        }

        public Task<IDbContextTransaction> BeginTransaction()
        {
            return _context.Database.BeginTransactionAsync();
        }

        public async Task<K> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            return entity.Id ; 
        }

        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
            return entities.Select(x => x.Id).ToList();  
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask ; 
        }

        public Task DeleteListAsync(IEnumerable<T> entites)
        {
            _context.Set<T>().RemoveRange(entites);
            return Task.CompletedTask ; 
        }

        public async Task EndTransactionAsync()
        {
            await SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
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


        public Task RollBackTransactionAsync()
        {
            return _context.Database.RollbackTransactionAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _unitOfWork.CommitAsync();
        }

        public Task UpdateAsync(T entity)
        {
            if(_context.Entry(entity).State == EntityState.Unchanged){
                return Task.CompletedTask ;
            }
            T exist = _context.Set<T>().Find(entity.Id); 
            _context.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask ; 
        }

        public async Task UpdateListAsync(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                await UpdateAsync(item);
            }  
        }

      
    }
}