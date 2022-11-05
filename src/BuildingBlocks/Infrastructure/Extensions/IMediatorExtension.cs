using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Event;
using Contracts.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class IMediatorExtension
    {
        public static async Task DispatchDomainEventAsync(this IMediator _mediator, IEnumerable<BaseEvent> domainEvents)
        {
            // var entites = context.ChangeTracker.Entries<IEventEntity>()
            //                                  .Select(x => x.Entity)
            //                                  .Where(x => x.DomainEvents.Any()).ToList();
            // var domainEvents = entites.SelectMany(x => x.DomainEvents).ToList();
            // entites.ForEach(x => x.ClearDomainEvent());
            foreach(var domainEvent in domainEvents){
                await _mediator.Publish(domainEvent);
            }
        }
    }
}