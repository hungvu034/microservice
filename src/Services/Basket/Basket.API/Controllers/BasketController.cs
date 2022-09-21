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

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private IBasketRepository _basketRepository ;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
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
            Cart result = await _basketRepository.UpdateBasketAsync(cart , options);
            return Ok(result);            
        }
        [HttpDelete("{username}")]
        public async Task<ActionResult<bool>> DeleteCard([FromRoute][Required]string userName){
            bool result = await _basketRepository.DeleteBasketFromUserNameAsync(userName);
            return Ok(result);
        }
    }
}