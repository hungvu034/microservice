using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Messages.Interfaces;
using EventBus.Messages.Intergration;

namespace EventBus.Messages.Event
{
    public record BasketCheckoutEvent() : IntergrationBaseEvent ,IBasketCheckoutEvent , IIntergrationEvent 
    {
        public string UserName { get ; set; }
        public decimal TotalPrice { get ; set ; }
        public string FirstName { get; set; }
        public string LastName { get ; set ; }
        public string EmailAddress { get; set; }
        public string ShippingAddress { get ; set; }
        public string InvoiceAddress { get; set ; }
    }
}