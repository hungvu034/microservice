using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Service.Mail;

namespace Contracts.Services
{
    public interface ISendMailService
    {
        Task SendMailAsync(MailRequest request);
    }
}