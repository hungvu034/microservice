using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.SeedWork;

namespace Shared.DTOs.Inventory
{
    public class GetInventoryPagingQuery : PagingRequestParameter 
    {
        public string? ItemNo { get; set; }
        public string? SearchTerm { get; set; }
    }
}