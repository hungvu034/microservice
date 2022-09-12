using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Domains
{
    public abstract class EntityAuditbase<T> : EntityBase<T>, IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; }
         public DateTimeOffset? LastModifiedDate { get ; set; }
    }
}