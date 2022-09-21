using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DTOs.Customer
{
    public class UpdateCustomerDto 
    {
        public string FirstName { get; set; }
        public string LastName { get; set ;}
        [EmailAddressAttribute]
        public string EmailAddress { get ; set; }
    }
}