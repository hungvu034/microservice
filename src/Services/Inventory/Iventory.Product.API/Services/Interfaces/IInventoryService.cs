using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Iventory.Product.API.Entites;
using Shared.DTOs.Inventory;
using Shared.SeedWork;

namespace Iventory.Product.API.Services.Interfaces
{
    public interface IInventoryService : IMongoDbRepositoryBase<InventoryEntry>
    {
        Task<IEnumerable<InventoryEntryDto>> GetAllByItemNoAsync(string itemNo);

        Task<PageList<InventoryEntryDto>> GetAllByItemNoPagingAsync(GetInventoryPagingQuery query);

        Task<InventoryEntryDto> GetByIdAsync(string Id);

        Task<InventoryEntryDto> PurchaseItemAsync(PurchaseProductDto model);

        Task DeleteByIdAsync(string Id);
    }
}