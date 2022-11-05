using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Service ;
using Infrastructure.Service.Mail;
using Shared.Service.Mail;
using Contracts.Services;
using MediatR;
using Ordering.Application.Features.V1.Email;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ISendMailService _sendMail ;
        private readonly IMediator _mediator ;

        public EmailController(ISendMailService sendMail, IMediator mediator)
        {
            _sendMail = sendMail;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<string> SendEmailAsync(MailRequest request){
            var result = await _mediator.Send(new SendMailRequest(){
                Request = request 
            });
            
            return result;
            
        }
    }
}