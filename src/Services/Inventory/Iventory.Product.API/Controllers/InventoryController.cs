using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Inventory;
using Iventory.Product.API.Services.Interfaces;
using Shared.SeedWork;

namespace Iventory.Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        /*
        GetAllByItemNo
        GetAllByItemNoPagingAsync
        GetInventoryById
        PurchaseOrder
        DelteById
        */
        private readonly IInventoryService _inventoryService ;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("items/{itemNo}")]
        public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetAllByItemNo([RequiredAttribute]string itemNo){
           var result = await _inventoryService.GetAllByItemNoAsync(itemNo);
           return Ok(result);
        }
        
        [HttpGet("items/{itemNo}/paging")]
        public async Task<ActionResult<PageList<InventoryEntryDto>>> GetAllByItemNoPagingAsync([FromRoute]string itemNo , [FromQuery]GetInventoryPagingQuery query){
            query.ItemNo = itemNo ; 
            var result = await _inventoryService.GetAllByItemNoPagingAsync(query);
            return result ;
        }

        [HttpGet("items/id/{id}")]
        public async Task<ActionResult<InventoryEntryDto>> GetInventoryById([Required]string id){
           var result = await _inventoryService.GetByIdAsync(id);
           return Ok(result);
        }

        [HttpPost("purchase/{itemNo}")]
        public async Task<ActionResult<InventoryEntryDto>> PurchaseOrder([Required]string itemNo , [FromBody]PurchaseProductDto model){
            model.ItemNo = itemNo ; 
            var result = await _inventoryService.PurchaseItemAsync(model);
            return Ok(result); 
        }

        [HttpDelete("delete/id/{id}")]
        public async Task<ActionResult> DeleteById(string id){
            var inventory = await _inventoryService.GetByIdAsync(id);
            if(inventory == null){
                return NotFound();
            }
            await _inventoryService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}