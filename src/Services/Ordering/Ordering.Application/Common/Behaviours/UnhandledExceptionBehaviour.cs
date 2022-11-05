using MediatR;
using Serilog;

namespace Ordering.Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TRespond> : IPipelineBehavior<TRequest, TRespond>
    where TRequest : IRequest<TRespond>
    {
        private readonly ILogger _logger;

        public UnhandledExceptionBehaviour(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TRespond> Handle(TRequest request, RequestHandlerDelegate<TRespond> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception e)
            {
                var requestName = typeof(TRequest).FullName;
                _logger.Error(e, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}