using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.Event;

namespace Basket.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<BasketCheckout,BasketCheckoutEvent>();
        }
    }
}