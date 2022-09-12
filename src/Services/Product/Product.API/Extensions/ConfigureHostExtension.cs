using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Extensions
{
    public static class ConfigureHostExtension
    {
        public static void AddAppConfiguration(this ConfigureHostBuilder host){
            host.ConfigureAppConfiguration(
                (context , config)=>{
                  string environmentName = context.HostingEnvironment.EnvironmentName;
                  config.AddJsonFile("appsettings.json" , false)
                  .AddJsonFile($"appsettings.{environmentName}.json" , optional : true , true)
                  .AddEnvironmentVariables()
                  ; 
                }
            );
        }
    }
}