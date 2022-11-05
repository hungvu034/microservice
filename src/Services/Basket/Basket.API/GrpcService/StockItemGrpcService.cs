using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcService
{
    public class StockItemGrpcService  
    {
        private StockService.StockServiceClient _stockServiceClient ;

        public StockItemGrpcService(StockService.StockServiceClient stockServiceClient)
        {
            _stockServiceClient = stockServiceClient;
        }
        public async Task<int> GetStockByItemNo(string itemNo){
            StockRequest request = new StockRequest(){
                ItemNo = itemNo 
            };
            var response = await _stockServiceClient.GetStockAsync(request);
            return response.Quantity ; 
        }
    }
}