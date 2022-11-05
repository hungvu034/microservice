using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shared.Service.Mail;

namespace Ordering.Application.Features.V1.Email
{
    public class SendMailRequest : IRequest<string> 
    {
       public MailRequest Request { get; set; } 
    }
}