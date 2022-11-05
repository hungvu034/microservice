using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Common;
using Inventory.Grpc.Entities;
using Inventory.Grpc.Repositories.Interfaces;
using MongoDB.Driver;

namespace Inventory.Grpc.Repositories
{
    public class InventoryRepository : MongoDbRepositoryBase<InventoryEntry>, IInventoryRepository
    {
        public InventoryRepository(IMongoClient client, Shared.Configurations.MongoDatabaseSettings settings) : base(client, settings)
        {
        }

        public int GetStockByItemNo(string itemNo)
        {
            var result = Collection.AsQueryable().Where(x => x.ItemNo == itemNo).Sum(x => x.Quantity);
            return result;
        }
    }
}