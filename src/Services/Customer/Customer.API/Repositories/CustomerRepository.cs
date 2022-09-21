using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class CustomerRepository : RepositoryBaseAsync<Entites.Customer,int,CustomerContext> ,  ICustomerRepository 
    {
        public CustomerRepository(CustomerContext context, IUnitofWork<CustomerContext> unitOfWork) : base(context, unitOfWork)
        {
            
        }
        public Task<Entites.Customer> GetCustomerByUserNameAsync(string UserName) => 
            base.FindByCondition(x => x.UserName == UserName).SingleOrDefaultAsync();

        
    }
}