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

        public async Task<long> CreateOrder(Order order)
        {
            long id = await CreateAsync(order);
            Console.WriteLine("VAO NE CreateOrder");
            return id ;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
          return await FindByCondition(x => x.UserName == userName).ToArrayAsync();
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            await UpdateAsync(order);
            await SaveChangesAsync();
            return order ; 
        }
    }
}