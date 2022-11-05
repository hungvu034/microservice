using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Contracts.Domains;

namespace Contracts.Common.Event
{
    public abstract class AudiableEntityEvent<T> : EventEntityBase<T> , IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}