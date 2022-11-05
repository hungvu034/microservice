using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Commands.CreateOrder;
using Ordering.Application.Features.V1.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.V1.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;
using Shared.SeedWork;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Order : ControllerBase
    {
        private readonly IMediator _mediatR ;
        public Order(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<ApiResult<IEnumerable<Order>>>> GetOrderByUserName([Required]string username){
            GetOrdersQuery query = new GetOrdersQuery(username);
            var result = await _mediatR.Send(query);
            return Ok(result) ; 
        }
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<long>>> CreateOrder([FromBody] CreateOrderCommand order){
            var result = await _mediatR.Send(order);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<ActionResult<ApiResult<OrderDto>>> UpdateOrder([FromBody] UpdateOrderCommand order){
            var result = await _mediatR.Send(order);
            return Ok(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<Unit>>> DeleteOrder([FromRoute][Required] long id){
            DeleteOrderCommand command = new DeleteOrderCommand(){
                Id = id
            };
            var result =  await _mediatR.Send(command) ; 
            return Ok(result);
        }
    }
}