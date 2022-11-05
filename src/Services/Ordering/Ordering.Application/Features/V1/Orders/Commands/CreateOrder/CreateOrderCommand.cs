using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Event;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Features.V1.Orders.Common;
using Ordering.Domain.Entites;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : CreateOrUpdateCommand , IRequest<ApiResult<long>> , IMapFrom<Order>
    {
        public string UserName { get; set; }

        public void Mapping(Profile profile){
            profile.CreateMap<CreateOrderCommand,Order>();
            profile.CreateMap<CreateOrderCommand,BasketCheckoutEvent>().ReverseMap();
         }
    }
}