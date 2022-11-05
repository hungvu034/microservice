using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand,ApiResult<Unit>>
    {
        private readonly IOrderRepository _repository;

        public DeleteOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResult<Unit>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _repository.FindByCondition(x => x.Id == request.Id).FirstOrDefault();
            if (order != null){
                await _repository.DeleteAsync(order);
                order.DeletedOrder();
                await _repository.SaveChangesAsync();
                return new ApiSuccessResult<Unit>(Unit.Value);
            }
            return new ApiResult<Unit>(false , "FALSE");
        }
    }
}