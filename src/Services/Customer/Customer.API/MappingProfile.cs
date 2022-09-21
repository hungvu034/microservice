using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Mapping;
using Shared.DTOs.Customer;

namespace Customer.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           this.CreateMap<Entites.Customer , CustomerDto>().ReverseMap();
           this.CreateMap< CreatedCustomerDto , Entites.Customer>();
           this.CreateMap< UpdateCustomerDto , Entites.Customer>().IgnoreAllNonExisting() ; 
        }
    }
}