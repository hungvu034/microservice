using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ordering.Application.Common.Mappings;
using Ordering.Domain.Entites;

namespace Ordering.Application.Features.V1.Orders.Common
{
    public class CreateOrUpdateCommand : IMapFrom<Order> 
    {
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ShippingAddress { get; set; }

        void Mapping(Profile profile) => profile.CreateMap(typeof(Order) , GetType()).ReverseMap();
        
    }
}