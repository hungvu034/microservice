using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.GrpcService;
using Basket.API.MassTransit;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using EventBus.Messages.Event;
using EventBus.Messages.Interfaces;
using EventBus.Messages.Services.Rabbitmq;
using Infrastructure.Common;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.Extensions.Caching.StackExchangeRedis ;
using Shared.Configurations;

namespace Basket.API.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services){
            services.AddTransient<ISerializeService , SerializeService>();
            services.AddScoped<IBasketRepository , BasketRepository>();
            services.AddScoped<RabbitMqService>();
            services.AddScoped<IPublish<BasketCheckoutEvent>,BasketCheckoutEventPublish>();
            return services; 
        }
        public static IServiceCollection ConfigureGrpcClient(this IServiceCollection collection){
            collection.AddGrpcClient<StockService.StockServiceClient>(
                factory => factory.Address = new Uri("http://localhost:5158")
            );
            collection.AddScoped<StockItemGrpcService>();
            return collection ; 
        }
        public static IServiceCollection ConfigureRedis(this IServiceCollection services , IConfiguration configuration){
           var connectionString = services.GetOptions<CacheSetting>("CacheSetting").ConnectionString;
           if(string.IsNullOrEmpty(connectionString)){
                throw new ArgumentNullException("Redis connection string is not configured");
           }
            return services.AddStackExchangeRedisCache(
                options => options.Configuration = connectionString 
            );
        }
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services){
            var settings = services.GetOptions<EventBusSettings>("EventBusSettings");
            if(settings == null){
                throw new ArgumentNullException("MassTransit does not configure");
            }
            Uri host = new Uri(settings.HostAddress);
            services.AddMassTransit(config => {
                config.UsingRabbitMq(
                    (ctx,cfg) => cfg.Host(host)
                );
                config.AddRequestClient<IBasketCheckoutEvent>();
            });
            return services;
        }
        public static IServiceCollection ConfigureMapper(this IServiceCollection service){
            service.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            return service ; 
        }
    }
}