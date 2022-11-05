using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Iventory.Product.API.Entites;
using Iventory.Product.API.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Shared.DTOs.Inventory;
using Shared.SeedWork;
using Infrastructure.Extensions ;
using Infrastructure.Common;
using MongoDatabaseSettings = Shared.Configurations.MongoDatabaseSettings ;
namespace Iventory.Product.API.Services
{
    public class InventoryService : MongoDbRepositoryBase<InventoryEntry>, IInventoryService
    {
        private IMapper _mapper;
        public InventoryService(IMongoClient client, MongoDatabaseSettings settings, IMapper mapper) : base(client, settings)
        {
            _mapper = mapper;
        
        }

        public async Task DeleteByIdAsync(string Id)
        {
            var deleteItem = await GetByIdAsync(Id) ; 
            await DeletedAsync(_mapper.Map<InventoryEntry>(deleteItem));
        }

        public async Task<IEnumerable<InventoryEntryDto>> GetAllByItemNoAsync(string itemNo)
        {
            var filter = Builders<InventoryEntry>.Filter.Eq(x => x.ItemNo, itemNo);
            var inventories = await Collection.Find(filter).ToListAsync();
            var result = _mapper.Map<IEnumerable<InventoryEntryDto>>(inventories);
            return result;
        }

        public async Task<PageList<InventoryEntryDto>> GetAllByItemNoPagingAsync(GetInventoryPagingQuery query)
        {
            var filter = Builders<InventoryEntry>.Filter.Eq(x => x.ItemNo, query.ItemNo);
            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                filter &= Builders<InventoryEntry>.Filter.Eq(x => x.DocumentNo, query.SearchTerm);
            }
            PageList<InventoryEntry> inventories = await Collection.PaginatedListAsync(filter,query.PageNumber , query.PageSize);
            var totalItems = inventories.MetaData.TotalItems ; 
            var result = new PageList<InventoryEntryDto>(_mapper.Map<IEnumerable<InventoryEntryDto>>(inventories) , query.PageNumber , query.PageSize , (int)totalItems);
            return result ; 
        }
        public async Task<InventoryEntryDto> GetByIdAsync(string Id)
        {
            var entity = await Collection.Find(Builders<InventoryEntry>.Filter.Eq(x => x.Id,Id)).FirstOrDefaultAsync();
            if(entity == null){
                return null ; 
            }
            return _mapper.Map<InventoryEntryDto>(entity);
        }

        public async Task<InventoryEntryDto> PurchaseItemAsync(PurchaseProductDto model)
        {
            InventoryEntry entity = new InventoryEntry(ObjectId.GenerateNewId().ToString()){
                ItemNo = model.ItemNo,
                Quantity = model.Quantity ,
            };
            await CreateAsync(entity);
            return _mapper.Map<InventoryEntryDto>(entity);
        }
    }
}