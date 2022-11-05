using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Domains;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Anotations;

namespace Inventory.Grpc.Entities
{
    [MongoDbCollectionAtributte("InventoryEntry")]
    public class InventoryEntry : MongoEntity
    {
        [BsonConstructor("ItemNo", "Quantity")]
        public InventoryEntry(string itemNo, int quantity)
        {
            ItemNo = itemNo;
            Quantity = quantity;
        }
        [BsonElement("itemNo")]
        public string ItemNo { get; set; }
        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}