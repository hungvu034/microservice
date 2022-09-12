using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository : RepositoryBaseAsync<CatalogProduct,long,ProductContext> , IProductRepository
    {
        public ProductRepository(ProductContext context , IUnitofWork<ProductContext> unitofWork) : base(context , unitofWork){

        }

        public  async Task<long> CreateProduct(CatalogProduct product)
        {
            return await  CreateAsync(product);
        }

        public async Task DeleteProduct(CatalogProduct product)
        {
             await base.DeleteAsync(product);
        }

        public async Task<CatalogProduct> GetProduct(long id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<CatalogProduct> GetProductByNo(string ProductNo)
        {
            return await base.FindByCondition(x => x.No == ProductNo).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CatalogProduct>> GetProducts()
        {
           return await base.FindAll().ToListAsync(); 
        }

        public async Task UpdateProduct(CatalogProduct product)
        {
            await base.UpdateAsync(product);
        }
    }
}