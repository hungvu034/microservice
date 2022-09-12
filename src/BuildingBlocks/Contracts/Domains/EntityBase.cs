using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Domains.Interfaces;

namespace Contracts.Domains
{
    public abstract class EntityBase<T> : IEntityBase<T>
    {
        public T Id { get; set; }
    }
}