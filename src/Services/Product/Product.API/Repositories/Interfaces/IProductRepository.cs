using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Common ; 
using Contracts.Common.Interfaces ;
using Product.API.Entities;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBaseAsync<CatalogProduct , long , ProductContext> 
    {
        Task<IEnumerable<CatalogProduct>> GetProducts() ;
        
        Task<CatalogProduct> GetProduct(long id); 

        Task<CatalogProduct> GetProductByNo(string ProductNo);

        Task<long> CreateProduct(CatalogProduct product);

        Task UpdateProduct(CatalogProduct product) ;
        Task DeleteProduct(CatalogProduct product);
    }
}