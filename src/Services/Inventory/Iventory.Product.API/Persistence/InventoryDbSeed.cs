using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iventory.Product.API.Entites;
using MongoDB.Driver;
using MongoDatabaseSettings = Shared.Configurations.MongoDatabaseSettings ;
namespace Iventory.Product.API.Persistence
{
    public class InventoryDbSeed
    {
        private readonly IMongoClient _mongoclient;
        private readonly MongoDatabaseSettings _settings;
        public InventoryDbSeed(IMongoClient mongoclient, MongoDatabaseSettings settings)
        {
            _mongoclient = mongoclient;
            _settings = settings;
        }

        public async Task SeedAsync()
        {
            var database = _mongoclient.GetDatabase(_settings.DatabaseName);
            var collection = database.GetCollection<InventoryEntry>("InventoryEntry");
            if (await collection.EstimatedDocumentCountAsync() == 0)
            {
                List<InventoryEntry> inventoryEntries = new(){
                    new InventoryEntry(){
                        ItemNo = "Lotus" ,
                        Quantity = 10 
                    },
                    new InventoryEntry(){
                        ItemNo = "Synchorous",
                        Quantity = 10 
                    }
                };
                await collection.InsertManyAsync(inventoryEntries);
            }
        }
    }
}