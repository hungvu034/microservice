using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBus.Messages.Intergration
{
    public interface IIntergrationEvent
    {
        public DateTime CreateDate { get; set; }

        public Guid Id { get; set; }
    }
}