using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext context , Serilog.ILogger logger) 
        {
            if(!context.Products.Any()){
                context.AddRange(getCatalogProduct()); 
                await context.SaveChangesAsync();
                logger.Information("Seeded data for ProductDb associated with context {DbContextName}",nameof(context)); 
            }
        }

        private static IEnumerable<CatalogProduct> getCatalogProduct()
        {
            IEnumerable<CatalogProduct> products = new List<CatalogProduct>(){
                new CatalogProduct(){
                    No = "Lotus" , 
                    Name = "Esprit" , 
                    Summary = "Nondisplaced frature of greater trochanter of right femur",
                    Description = "Nondisplaced frature of greater trochanter of right femur" , 
                    Price = (decimal)177940.49
                },
                new CatalogProduct(){
                     No = "Synchorous" , 
                    Name = "Endecate" , 
                    Summary = "Alisan  of greater trochanter of right femur",
                    Description = "Alisan  of greater trochanter of right femur" , 
                    Price = (decimal)87794.49    
                }
            };
            return products ; 
        }
    }
}