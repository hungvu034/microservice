using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;
using Contracts.Common.Event;
using Ordering.Domain.OrderAggregate.Events;

namespace Ordering.Domain.Entites
{
    public class Order : AudiableEntityEvent<long>
    {
        public Order()
        {

        }

        public Order(string userName, decimal totalPrice, string firstName, string lastName, string emailAddress, string shippingAddress)
        {
            this.UserName = userName;
            this.TotalPrice = totalPrice;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.ShippingAddress = shippingAddress;
        }
        public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid DocumentNo { get; set; } = Guid.NewGuid();

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress()]
        public string EmailAddress { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string InvoiceAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_invoidAddress))
                {
                    return ShippingAddress;
                }
                return _invoidAddress ; 
            }
            set
            {
                _invoidAddress = value ;
            }
        }

        private string _invoidAddress;
        [NotMapped]
        public string FullName => FirstName + " " + LastName;
        public Order AddedOrder()
        {
            AddDomainEvent(new OrderCreatedEvent(Id, UserName, DocumentNo.ToString(), EmailAddress, TotalPrice, ShippingAddress, InvoiceAddress));

            return this;
        }

        public Order DeletedOrder()
        {
            AddDomainEvent(new OrderDeletedEvent(Id,UserName,DocumentNo.ToString(),EmailAddress,TotalPrice,ShippingAddress,InvoiceAddress));
            return this;
        }
    }
}