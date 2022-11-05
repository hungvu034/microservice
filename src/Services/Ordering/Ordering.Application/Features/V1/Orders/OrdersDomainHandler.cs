using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Contracts.Services;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.OrderAggregate.Events;
using Serilog;
using Shared.Service.Mail;

namespace Ordering.Application.Features.V1.Orders
{
    public class OrdersDomainHandler : INotificationHandler<OrderCreatedEvent> , INotificationHandler<OrderDeletedEvent>
    {
        private readonly ILogger _logger ;
        private readonly ISerializeService _serializeService ;
        private readonly ISendMailService _sendMailService ;
        
        private readonly IOrderRepository _repository ;
        public OrdersDomainHandler(ILogger logger, ISerializeService serializeService, ISendMailService sendMailService, IOrderRepository repository)
        {
            _logger = logger;
            _serializeService = serializeService;
            _sendMailService = sendMailService;
            _repository = repository;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var obj = _serializeService.Serialize(notification);
            _logger.Information("Ordering Domain Event: {OrderCreatedEvent}" + " " + obj);

            MailRequest mailRequest = new MailRequest();
            mailRequest.To.Add(notification.EmailAddress);
            mailRequest.Subject = "Ordering Created";
            mailRequest.Body = "Ordering Created Success: " + obj ;  
            mailRequest.Cc = "hungvu034@gmail.com" ; 
            await _sendMailService.SendMailAsync(mailRequest);

            _logger.Information("Send mail success");
        }

        public async Task Handle(OrderDeletedEvent notification, CancellationToken cancellationToken)
        {
            var obj = _serializeService.Serialize(notification);
            _logger.Information("Ordering doamin Event: {OrderDeletedEvent}" + " " + obj);

            MailRequest mailRequest = new MailRequest();
            mailRequest.To.Add(notification.EmailAddress);
            mailRequest.Subject = "Ordering Deleled";
            mailRequest.Body = "Ordering Deleted Success: " + obj ;  
            mailRequest.Cc = "hungvu034@gmail.com" ;

            await _sendMailService.SendMailAsync(mailRequest);
            _logger.Information("Send mail success");
        }
    }
}