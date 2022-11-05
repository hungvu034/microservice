using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Ordering.Application.Features.V1.Orders.Common;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator(){
          Include(new CreateOrUpdateValidator());
          RuleFor(x => x.Id).NotNull(); 
        }
    }
}