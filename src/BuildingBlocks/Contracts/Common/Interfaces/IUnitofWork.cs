using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Contracts.Common.Interfaces
{
    public interface IUnitofWork<TContext> : IDisposable where TContext : DbContext
    {
        Task<int> CommitAsync();        
    }
}