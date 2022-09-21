using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DTOs.Customer
{
    public class CreatedCustomerDto 
    {
        [Required()]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set ;}
        [EmailAddress]
        public string EmailAddress { get ; set; }
    }
}