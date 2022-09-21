using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public static class CustomerContextSeed
    {
        public static IHost SeedingCustomer(this IHost host){
            CustomerContext context =  host.Services.CreateScope().ServiceProvider.GetRequiredService<CustomerContext>();
            context.Database.Migrate();
            if(context.Customers.Count() == 0){
                SeedingData(context);
            }
            return host ; 
            
        }
        private static void SeedingData(CustomerContext context){
            context.AddRange(
                new Entites.Customer(){
                    UserName = "hungvu034",
                    EmailAddress = "hungvu034@gmail.com",
                    FirstName = "Vu",
                    LastName = "Hung", 
                },
                new Entites.Customer(){
                    UserName = "tomcat",
                    EmailAddress = "tomcat@gmail.com",
                    FirstName = "Tom",
                    LastName = "Cat", 
                },
                new Entites.Customer(){
                    UserName = "meotom",
                    EmailAddress = "meotom@gmail.com",
                    FirstName = "Tom",
                    LastName = "Meo", 
                }                
            );
            context.SaveChanges();
        }
    }
}