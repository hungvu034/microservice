using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryQueryBase< Entites.Customer, int , CustomerContext>
    {
        Task<Entites.Customer> GetCustomerByUserNameAsync(string UserName);

        Task<int> CreateAsync(Entites.Customer customer); 

        Task DeleteAsync(Entites.Customer customer);

        Task UpdateAsync(Entites.Customer customer);
    }
}