using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Extensions
{
    public static class HostExtension
    {
        public static ConfigureHostBuilder AddAppConfiguration(this ConfigureHostBuilder host){
            host.ConfigureAppConfiguration(
                (context , builder) => {
                    string hostName = context.HostingEnvironment.EnvironmentName ; 
                    builder.AddJsonFile("appsettings.json" , false , true)
                            .AddJsonFile($"appsetting.{hostName}.json" , true , true)
                            .AddEnvironmentVariables() ; 
                }
            );
            return host ; 
        }
    }
}