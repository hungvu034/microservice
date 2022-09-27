using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace Ordering.Domain.Entites
{
    public class Order : EntityAuditbase<long>
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

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress()]
        public string EmailAddress { get; set; }

        [Required]
        public string ShippingAddress { get; set; }


    }
}