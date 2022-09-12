using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Domains;

namespace Product.API.Entities
{
    public class CatalogProduct : EntityAuditbase<long>
    {

        [RequiredAttribute()]
        [ColumnAttribute(TypeName = "varchar(50)")]
        public string No { get; set; }

        [RequiredAttribute()]
        [ColumnAttribute(TypeName = "varchar(250)")]
        public string Name { get; set ;}


        [ColumnAttribute(TypeName = "varchar(250)")]
        public string Summary { get; set; }

        [ColumnAttribute(TypeName = "text")]
        public string Description { get; set; }

        public decimal Price { get; set; }

    }
}