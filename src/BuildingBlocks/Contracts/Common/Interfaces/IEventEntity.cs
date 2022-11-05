using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Event;
using Contracts.Domains.Interfaces;

namespace Contracts.Common.Interfaces
{
    public interface IEventEntity
    {
       void AddDomainEvent(BaseEvent domainEvent);
       void RemoveDomainEvent(BaseEvent domainEvent);
       void ClearDomainEvent();
       IReadOnlyCollection<BaseEvent> DomainEvents { get; }
    }
    public interface IEventEntity<T> : IEntityBase<T> , IEventEntity{

    }

}