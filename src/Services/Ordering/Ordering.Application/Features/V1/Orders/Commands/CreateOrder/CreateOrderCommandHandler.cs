using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entites;
using Shared.SeedWork;
using Serilog;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        private readonly ILogger _logger ;
        public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = _mapper.Map<Order>(request);
            ApiResult<long> result ; 
            try
            {
                long id = await _repository.CreateOrder(order);
                order.AddedOrder(); 
                _logger.Information("OrderCreatedEvent is added in order by username: " + order.UserName);
                await _repository.SaveChangesAsync();
                result = new ApiResult<long>(true , "SUCCESS" , id);
            }
            catch(Exception e){
                string message = e.Message ; 
                var requestName = e.GetType().FullName;
                _logger.Error(e, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
                if(e is DbUpdateException dbUpdateException){
                   message =  e.InnerException.Message ; 
                }
                result = new ApiResult<long>(false , message );
            }
            return result ; 
        }
    }
}