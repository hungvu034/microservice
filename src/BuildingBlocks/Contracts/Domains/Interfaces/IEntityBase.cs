using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Domains.Interfaces
{
    public interface IEntityBase<T>
    {
        T Id { get ; set; }
    }
}