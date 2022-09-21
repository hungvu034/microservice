using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Services.Interfaces
{
    public interface ICustomerService
    {
        IResult GetCustomerByUserName(string UserName);

        IEnumerable<Entites.Customer> GetAllCustomers(); 

         Task<int> CreateCustomer(Entites.Customer customer);
         Task DeleteCustomer(Entites.Customer customer);
         Task UpdateCustomer(Entites.Customer customer);

         
    } 
}