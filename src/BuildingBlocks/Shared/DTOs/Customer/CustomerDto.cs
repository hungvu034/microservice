using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set ;}
        [EmailAddress]
        public string EmailAddress { get ; set; }
    }
}