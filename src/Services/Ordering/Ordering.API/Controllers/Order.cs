using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;

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
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserName([Required]string username){
            GetOrdersQuery query = new GetOrdersQuery(username);
            var result = await _mediatR.Send(query);
            return Ok(result) ; 
        }
    }
}