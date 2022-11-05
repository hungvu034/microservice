using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Messages.Interfaces;
using RabbitMQ.Client;
using EventBus.Messages.Services.Rabbitmq;
using RabbitMQ.Client.Events;
using Contracts.Common.Interfaces;
using EventBus.Messages.Event;
using MediatR;
using ILogger = Serilog.ILogger ;
using AutoMapper;
using Ordering.Application.Features.V1.Orders.Commands.CreateOrder;

namespace Ordering.API.Application.MassTransit
{
    public class BasketCheckoutEventConsumer : IConsume
    {
        private RabbitMqService _rabbitmqService;
        private static readonly string EXCHANGE_NAME = "basket-checkout-event";
        private readonly ISerializeService _serializeService ; 
        private readonly IMediator _mediator;
        private readonly IMapper _mapper ; 
        private readonly ILogger _logger ;
        public BasketCheckoutEventConsumer(RabbitMqService rabbitmqService, ISerializeService serializeService, IMediator mediator, ILogger logger, IMapper mapper)
        {
            _rabbitmqService = rabbitmqService;
            _serializeService = serializeService;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public void Consume()
        {
            var chanel = _rabbitmqService.CreateChanel();
            chanel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Fanout);
            chanel.QueueDeclare("basket-checkout-queue");
            chanel.QueueBind("basket-checkout-queue", EXCHANGE_NAME, "");
            EventingBasicConsumer consumer = new EventingBasicConsumer(chanel);
            consumer.Received += async (sender,e) => {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                var basketCheckoutEvent = _serializeService.Deserialize<BasketCheckoutEvent>(message);
                CreateOrderCommand command = _mapper.Map<CreateOrderCommand>(basketCheckoutEvent);
                try{
                    await _mediator.Send(command);
                }
                catch{
                    
                }
                _logger.Information("Message received");
            };
            chanel.BasicConsume("basket-checkout-queue",true,consumer);
        }
    }
}