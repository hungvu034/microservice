using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Entities;
using RabbitMQ.Client;
using Contracts.Common.Interfaces;
using EventBus.Messages.Interfaces;
using EventBus.Messages.Services.Rabbitmq;
using EventBus.Messages.Event;

namespace Basket.API.MassTransit
{
    public class BasketCheckoutEventPublish : IPublish<BasketCheckoutEvent>
    {
        private readonly ISerializeService _serializeService ;
        private readonly RabbitMqService _rabbitmqService ;

        public BasketCheckoutEventPublish(ISerializeService serializeService, RabbitMqService rabbitmqService)
        {
            _serializeService = serializeService;
            _rabbitmqService = rabbitmqService;
        }

        public void Publish(BasketCheckoutEvent message)
        {
            var chanel = _rabbitmqService.CreateChanel();
            chanel.ExchangeDeclare("basket-checkout-event", "fanout",false,false,null);
            string jsonMessage = _serializeService.Serialize(message);
            byte[] body = Encoding.UTF8.GetBytes(jsonMessage);
            chanel.BasicPublish("basket-checkout-event","",false,null,body);
        }
    }
}