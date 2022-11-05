using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Common;
using Shared.SeedWork;
using Ordering.Domain.Entites;
using Infrastructure.Mapping;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResult<OrderDto>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order currentOrder = await _repository.GetByIdAsync(request.Id);
            Order updateOrder = _mapper.Map(request, currentOrder);
            try
            {
                await _repository.UpdateOrder(updateOrder);
                ApiResult<OrderDto> result = new ApiResult<OrderDto>(true, "SUCCESS", _mapper.Map<OrderDto>(updateOrder));
                return result;
            }
            catch
            {
                ApiResult<OrderDto> result = new ApiResult<OrderDto>(false , "FALSE");
                return result;
            }
        }
    }
}