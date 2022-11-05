using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Iventory.Product.API.Entites;
using Shared.DTOs.Inventory;

namespace Iventory.Product.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<InventoryEntry,InventoryEntryDto>().ReverseMap();
            CreateMap<PurchaseProductDto , InventoryEntryDto>();

        }
    }
}