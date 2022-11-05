using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Event;

namespace Ordering.Domain.OrderAggregate.Events
{
    public class OrderDeletedEvent : BaseEvent
    {
        public OrderDeletedEvent(long id, string userName, string documentNo, string emailAddress, decimal totalPrice, string shippingAddress, string invoiceAddress)
        {
            Id = id;
            UserName = userName;
            DocumentNo = documentNo;
            EmailAddress = emailAddress;
            TotalPrice = totalPrice;
            ShippingAddress = shippingAddress;
            InvoiceAddress = invoiceAddress;
        }

        public long Id { get; private set; }
        public string UserName { get; private set; }
        public string DocumentNo { get; private set; }
        public string EmailAddress { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string ShippingAddress { get; private set; }
        public string InvoiceAddress { get; private set; }

     
    }
}