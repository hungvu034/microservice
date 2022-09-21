using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.API.Entites;

namespace Customer.API.Services.Interfaces
{
    public interface IMinimalValidator
    {
        ValidatorResult Validate<T>(T entity) ;  
    }
}