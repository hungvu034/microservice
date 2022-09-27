using FluentValidation;
using MediatR ; 
namespace Ordering.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TRespond> : IPipelineBehavior<TRequest, TRespond>
    where TRequest : IRequest<TRespond>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator ;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }

        public async Task<TRespond> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TRespond> next)
        {
            if(!_validator.Any())
                return await next();
            var context = new ValidationContext<TRequest>(request);

            var validationResult = await Task.WhenAll(_validator.Select(async v => await v.ValidateAsync(context,cancellationToken)));
            
            if(!validationResult.Any())
                return await next();

            var failures = validationResult.Where(r => r.Errors.Any())
                                            .SelectMany(r => r.Errors);
                                            
            throw new ValidationException(failures);
        }
    }
}