using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
    {
        private readonly IMapper _mapper ; 
        private readonly IOrderRepository _repository ;

        public GetOrdersQueryHandler(IMapper mapper, IOrderRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            string userName = request.UserName ; 
            var orders = await _repository.GetOrdersByUserName(userName);
            var orderdtos = _mapper.Map<List<OrderDto>>(orders.ToList());
            return new ApiSuccessResult<List<OrderDto>>(orderdtos);
        }
    }
}