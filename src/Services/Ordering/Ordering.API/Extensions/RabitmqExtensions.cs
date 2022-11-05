using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Messages.Interfaces;

namespace Ordering.API.Extensions
{
    public static class RabitmqExtensions
    {
        public static void LisenMessage(this IHost host){
            var scope = host.Services.CreateScope();
            var consumer =  scope.ServiceProvider.GetRequiredService<IConsume>();
            consumer.Consume();
        }
    }
}