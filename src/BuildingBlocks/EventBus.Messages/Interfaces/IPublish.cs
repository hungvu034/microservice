using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace EventBus.Messages.Interfaces
{
    public interface IPublish
    {
        void Publish(); 
    }

    public interface IPublish<T>{
        void Publish(T message) ;
    }
}