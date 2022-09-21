using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;

namespace Customer.API.Services
{
    public class CustomerAdvanceService : CustomerService, ICustomerAdvanceService
    {
        public CustomerAdvanceService(ICustomerRepository repository) : base(repository)
        {

        }

        public Entites.Customer FindCustomerById(int id)
        {
           return _repository.FindByCondition(x => x.Id == id).FirstOrDefault();
        }

        public Entites.Customer FindCustomerByUserName(string username)
        {
             return _repository.FindByCondition(x => x.UserName == username).FirstOrDefault();
        }

        public string GetFullNameByUserName(string username)
        {
            var customer = _repository.FindByCondition(x => x.UserName == username).FirstOrDefault();
            if(customer == null)
                return string.Empty  ;
            return customer.FirstName + " " + customer.LastName ; 
        }

        public bool UpdateEmailByUserName(string email, string username)
        {
            var customer = _repository.FindByCondition(  x => x.UserName == username).FirstOrDefault();
            if(customer == null){
                return false ; 
            }
            customer.EmailAddress = email ; 
            _repository.UpdateAsync(customer).Wait();
            
            return true ; 
        }
    }
}