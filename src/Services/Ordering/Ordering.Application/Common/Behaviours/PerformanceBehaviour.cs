using System.Diagnostics;
using MediatR ;
using Serilog ; 

namespace Ordering.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TRespond> : IPipelineBehavior<TRequest, TRespond> 
    where TRequest : IRequest<TRespond>  
    {
        
        private readonly Stopwatch _timer ; 
        private readonly ILogger _logger;

        public PerformanceBehaviour(ILogger logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TRespond> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TRespond> next)
        {
            _timer.Start();
            var respond = await next();
            _timer.Stop();
            long ElapsedMilliseconds = _timer.ElapsedMilliseconds ;
            if(ElapsedMilliseconds <= 500)
                return respond ; 
            string requestName = typeof(TRequest).FullName ; 
            _logger.Warning($"Application Long running request {requestName} : {ElapsedMilliseconds} miliseconds") ;
            return respond ;                                                                                                                                                                                                                                                                                                                            

        }
    }                                                                                       
}