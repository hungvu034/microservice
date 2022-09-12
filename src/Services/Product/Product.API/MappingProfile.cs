using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Mapping;
using Product.API.Entities;
using Shared.DTOs.Product;

namespace Product.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<CatalogProduct,ProductDto>();
            CreateMap<CreateProductDto , CatalogProduct>().ReverseMap();
            CreateMap<UpdateProductDto, CatalogProduct>().IgnoreAllNonExisting() ;
        }
    }
}