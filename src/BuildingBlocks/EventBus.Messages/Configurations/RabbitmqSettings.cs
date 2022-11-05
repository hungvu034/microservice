using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBus.Messages.Configurations
{
    public class RabbitmqSettings
    {
        public string HostName { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}