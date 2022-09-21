using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Services.Interfaces
{
    public interface ICustomerAdvanceService : ICustomerService
    {
        bool UpdateEmailByUserName(string email , string username);

        string GetFullNameByUserName(string username);

        Entites.Customer FindCustomerById(int id) ; 

        Entites.Customer FindCustomerByUserName(string username);
    }
}