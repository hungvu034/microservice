using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Inventory.Grpc.Repositories.Interfaces;

namespace Inventory.Grpc.Services
{
    public class GetStockService : StockService.StockServiceBase 
    {
        private IInventoryRepository _inventoryRepository ;

        public GetStockService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public override async Task<StockResponse> GetStock(StockRequest request, ServerCallContext context)
        {
            var quantity = _inventoryRepository.GetStockByItemNo(request.ItemNo);
            var result = new StockResponse(){
                Quantity = quantity 
            };
            return result ; 
        }

    }
}