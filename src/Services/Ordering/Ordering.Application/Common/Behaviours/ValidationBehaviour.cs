using FluentValidation;
using MediatR;
using Serilog;

namespace Ordering.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TRespond> : IPipelineBehavior<TRequest, TRespond>
    where TRequest : IRequest<TRespond>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;
        private readonly ILogger _logger ;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validator, ILogger logger)
        {
            _validator = validator;
            _logger = logger;
        }

        public async Task<TRespond> Handle(TRequest request, RequestHandlerDelegate<TRespond> next, CancellationToken cancellationToken)
        {
            if (!_validator.Any())
                return await next();
            var context = new ValidationContext<TRequest>(request);

            var validationResult = await Task.WhenAll(_validator.Select(async v => await v.ValidateAsync(context, cancellationToken)));

            if (!validationResult.Any())
                return await next();
            
            var failures = validationResult.Where(r => r.Errors.Any())
                                            .SelectMany(r => r.Errors);
            _logger.Information(failures.ToArray().Length.ToString());
               throw new ValidationException(failures);
        }
    }
}