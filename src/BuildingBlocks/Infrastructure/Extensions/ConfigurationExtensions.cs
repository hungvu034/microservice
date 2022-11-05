using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetOptions<T>(this IServiceCollection services , string sectionName)
        where T : new() 
        {
           var service = services.BuildServiceProvider();
           var configure = service.GetRequiredService<IConfiguration>();
           var section = configure.GetSection(sectionName);
           var options = new T();
            section.Bind(options);
            return options ;
        }
    }
}