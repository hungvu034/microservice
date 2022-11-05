using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBus.Messages.Intergration
{
    public record IntergrationBaseEvent() : IIntergrationEvent
    {
        public DateTime CreateDate { get ; set; } = DateTime.Now  ;
        public Guid Id { get ; set ; }
    }
}