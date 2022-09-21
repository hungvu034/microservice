using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json ; 
using ILogger = Serilog.ILogger  ;
namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService ;
        private readonly ISerializeService _serializeService ;

        private readonly ILogger _logger ; 
        public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService , ILogger logger)
        {
            _redisCacheService = redisCacheService;
            _serializeService = serializeService;
            _logger = logger ;
        }

        public async Task<bool> DeleteBasketFromUserNameAsync(string userName)
        {
            try{
                await _redisCacheService.RemoveAsync(userName);
                return true ; 
            }
            catch{
                return false ; 
            }
        }

        public async Task<Cart> GetBasketByUserNameAsync(string userName)
        {
            string CartString = await _redisCacheService.GetStringAsync(userName) ; 
            if(string.IsNullOrEmpty(CartString)){
                return null ;
            }
            return _serializeService.Deserialize<Cart>(CartString);
        }

        public async Task<Cart> UpdateBasketAsync([FromBody()]Cart cart, DistributedCacheEntryOptions options = null){
            string cartString = _serializeService.Serialize(cart);
            if(options != null)
               await _redisCacheService.SetAsync(cart.UserName , System.Text.Encoding.UTF8.GetBytes(cartString) , options);
            else
               await _redisCacheService.SetAsync(cart.UserName , System.Text.Encoding.UTF8.GetBytes(cartString));
            _logger.Information(cartString);
            return await GetBasketByUserNameAsync(cart.UserName);
        }
    }
}