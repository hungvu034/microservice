using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Domains;

namespace Customer.API.Entites
{
    public class Customer : EntityBase<int>
    {
        [RequiredAttribute]
        public string UserName { get; set; }
        [Required]
        [ColumnAttribute(TypeName = "varchar(100)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set ;}

        [Required]
        [EmailAddress]
        public string EmailAddress { get ; set; }
    }
}