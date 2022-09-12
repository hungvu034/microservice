using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;

namespace Product.API.Persistence
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        } 
        public DbSet<CatalogProduct> Products { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var Entites = ChangeTracker.Entries().Where(entity => entity.State == EntityState.Added 
                                                    || entity.State == EntityState.Modified 
                                                    || entity.State == EntityState.Deleted );
            foreach(var item in Entites){
                switch(item.State){
                    case EntityState.Added:
                        if(item.Entity is IDateTracking addedEntity){
                            addedEntity.CreatedDate = DateTimeOffset.UtcNow ; 
                            item.State = EntityState.Added ; 
                        }
                        break;
                    case EntityState.Modified:
                        Entry(item.Entity).Property("Id").IsModified = false ; 
                        if(item.Entity is IDateTracking modifiedEntity){
                            modifiedEntity.LastModifiedDate = DateTimeOffset.UtcNow;
                            item.State = EntityState.Modified ; 
                        }
                        break ; 
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}