using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Configurations
{
    public class MongoDatabaseSettings : DatabaseSettings
    {
        public string DatabaseName { get; set; }
    }
}