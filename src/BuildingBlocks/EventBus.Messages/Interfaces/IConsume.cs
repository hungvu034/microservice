using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace EventBus.Messages.Interfaces
{
    public interface IConsume
    {
        void Consume(); 
    }
}