using MediatR;
using Ordering.Application.Common.Models;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<ApiResult<List<OrderDto>>>
    {
        public GetOrdersQuery(string userName)
        {
            UserName = userName ?? throw  new ArgumentNullException(nameof(UserName));
        }

        public string UserName {  get;  private set; }
    }
}