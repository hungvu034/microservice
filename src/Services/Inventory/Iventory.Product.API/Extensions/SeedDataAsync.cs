using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iventory.Product.API.Persistence;

namespace Iventory.Product.API.Extensions
{
    public static class SeedDataAsync
    {
        public static void SeedData(this IHost host){
            var inventoryDbSeed = host.Services.GetRequiredService<InventoryDbSeed>();
            inventoryDbSeed.SeedAsync().Wait();
        }
    }
}