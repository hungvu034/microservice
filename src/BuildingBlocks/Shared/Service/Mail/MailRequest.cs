using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Service.Mail
{
    public class MailRequest
    {
        public string Body { get; set; }

        public string Cc { get; set; } 

        public List<string> To { get; set; } = new List<string>();

        public string Subject { get; set; }
    }
}