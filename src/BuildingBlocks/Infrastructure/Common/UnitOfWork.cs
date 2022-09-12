using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public class UnitOfWork<TContext> : IUnitofWork<TContext> where TContext : DbContext
    {
        private TContext _context ; 
        
        public UnitOfWork(TContext context){
            _context = context ; 
        }
        public Task<int> CommitAsync()
        {
           return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}