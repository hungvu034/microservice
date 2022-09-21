using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.Extensions.Caching.StackExchangeRedis ; 
namespace Basket.API.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services){
            services.AddTransient<ISerializeService , SerializeService>();
            services.AddScoped<IBasketRepository , BasketRepository>();
            return services; 
        }

        public static IServiceCollection ConfigureRedis(this IServiceCollection services , IConfiguration configuration){
           var connectionString = configuration.GetSection("CacheSetting:ConnectionString").Value;
           if(string.IsNullOrEmpty(connectionString)){
                throw new ArgumentNullException("Redis connection string is not configured");
           }
            return services.AddStackExchangeRedisCache(
                options => options.Configuration = connectionString 
            );
        }
    }
}