using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public class CustomerContext : DbContext
    {
    
        public DbSet<Customer.API.Entites.Customer> Customers { get; set; }
       public CustomerContext(DbContextOptions<CustomerContext> options) : base(options){
        
       }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entites.Customer>().HasIndex( x => x.UserName).IsUnique();
            modelBuilder.Entity<Entites.Customer>().HasIndex( x => x.EmailAddress).IsUnique();
        }
    }
}