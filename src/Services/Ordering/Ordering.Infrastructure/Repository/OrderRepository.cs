using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entites;
using Ordering.Infrastructure.Persisten;

namespace Ordering.Infrastructure.Repository
{
    public class OrderRepository :RepositoryBaseAsync<Domain.Entites.Order, long, OrderingContext> , IOrderRepository 
    {
        public OrderRepository(OrderingContext context, IUnitofWork<OrderingContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
          return await FindByCondition(x => x.UserName == userName).ToArrayAsync();
        }
    }
}