using System.Data.Common;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using AutoMapper;
using MassTransit;
using EventBus.Messages.Event;
using EventBus.Messages.Interfaces;
using Basket.API.GrpcService;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private IBasketRepository _basketRepository ;
        private IMapper _mapper ;
        private IPublishEndpoint _publishEndpoint ;
        private StockItemGrpcService _stockItemGrpcService ;  
        private IPublish<BasketCheckoutEvent> _publisher ;
        public BasketController(IBasketRepository basketRepository, IMapper mapper, IPublishEndpoint publishEndpoint, IPublish<BasketCheckoutEvent> publisher, StockItemGrpcService stockItemGrpcService)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _publisher = publisher;
            _stockItemGrpcService = stockItemGrpcService;
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<Cart>> GetCartByUserName([FromRoute][RequiredAttribute]string username){
            var result = await _basketRepository.GetBasketByUserNameAsync(username);
            return Ok(result ?? new Cart());
        }
        [HttpPost("updateCart")]
        public async Task<ActionResult<Cart>> UpdateCard([FromBody]Cart cart){
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                                                   .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1))
                                                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            foreach(var cartItem in cart.Items){
                cartItem.AvailableQuantity = await _stockItemGrpcService.GetStockByItemNo(cartItem.ItemNo);
            }
            Cart result = await _basketRepository.UpdateBasketAsync(cart , options);
            return Ok(result);            
        }
        [HttpDelete("{username}")]
        public async Task<ActionResult<bool>> DeleteCard([FromRoute][Required]string userName){
            bool result = await _basketRepository.DeleteBasketFromUserNameAsync(userName);
            return Ok(result);
        }
        
        [HttpPost("/[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Checkout([FromBody]BasketCheckout basketCheckout){
            var basket = await _basketRepository.GetBasketByUserNameAsync(basketCheckout.UserName);
            if(basket == null){
                return NotFound();
            }
            BasketCheckoutEvent basketCheckoutEvent = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            basketCheckoutEvent.TotalPrice = basket.TotalPrice ; 
            _publisher.Publish(basketCheckoutEvent);
             // await _publishEndpoint.Publish(basketCheckoutEvent);
            await _basketRepository.DeleteBasketFromUserNameAsync(basketCheckout.UserName);
            return Accepted();
        }
    }
}