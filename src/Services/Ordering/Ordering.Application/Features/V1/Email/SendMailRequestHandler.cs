using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Services;
using MediatR;

namespace Ordering.Application.Features.V1.Email
{
    public class SendMailRequestHandler : IRequestHandler<SendMailRequest,string>
    {
        private readonly ISendMailService _service ;

        public SendMailRequestHandler(ISendMailService service)
        {
            _service = service;
        }
        public async Task<string> Handle(SendMailRequest request, CancellationToken cancellationToken)
        {
            try{
               await _service.SendMailAsync(request.Request);
               return "Success" ; 
            }
            catch(Exception e){
                return e.Message ;
            }
        }

    }
}