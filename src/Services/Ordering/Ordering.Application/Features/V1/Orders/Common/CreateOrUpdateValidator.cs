using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Ordering.Application.Features.V1.Orders.Common
{
    public class CreateOrUpdateValidator : AbstractValidator<CreateOrUpdateCommand>
    {
        public CreateOrUpdateValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{FirstName} is required")
                                     .NotNull()
                                     .MaximumLength(50).WithMessage("{FirstName} must not exceed 50 characters");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{LastName} is required")
                         .NotNull()
                         .MaximumLength(50).WithMessage("{LastName} must not exceed 50 characters");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Must be Email");
            RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage("ShippingAdress not empty")
                                            .NotNull().WithMessage("ShippingAdress not null");
        }
    }
}