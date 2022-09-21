using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;

namespace Customer.API.Services
{
    public class CustomerService : ICustomerService 
    {
        protected ICustomerRepository _repository ;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<int> CreateCustomer(Entites.Customer customer)
        {
           return await _repository.CreateAsync(customer);
        }

        public async Task DeleteCustomer(Entites.Customer customer)
        {
            await _repository.DeleteAsync(customer);
        }

        public IEnumerable<Entites.Customer> GetAllCustomers()
        {
            return _repository.FindAll();
        }

        public IResult GetCustomerByUserName(string UserName)
        {
            return Results.Ok(_repository.FindByCondition(x => x.UserName == UserName)); 
        }

            public async Task UpdateCustomer(Entites.Customer customer)
        {
           await _repository.UpdateAsync(customer);
        }
    }
}