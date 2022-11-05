using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Services;
using EventBus.Messages.Event;
using MassTransit;
using MediatR;
using Ordering.Application.Features.V1.Orders.Commands.CreateOrder;
using Shared.Service.Mail;

namespace Ordering.API.Application.IntegrationEvents.EventHandler
{
    public class BasketCheckoutEventHandler : IConsumer<BasketCheckoutEvent>
    {
        private  IMediator _mediator  ;
        private IMapper _mapper ;

        private ISendMailService _mailService ;
        public BasketCheckoutEventHandler(IMediator mediator, IMapper mapper, ISendMailService mailService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            CreateOrderCommand command = _mapper.Map<CreateOrderCommand>(context.Message);
            try{
                await _mediator.Send(command);
                MailRequest request = new MailRequest();
                request.To.Add(command.EmailAddress) ; 
                request.Body = "Create Success" ; 
                _mailService.SendMailAsync(request);
            }
            catch{

            }

        }
    }
}