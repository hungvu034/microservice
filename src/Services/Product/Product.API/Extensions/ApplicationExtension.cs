using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Extensions
{
    public static class ApplicationExtension
    {
        public static void UseInfrastructure(this IApplicationBuilder app){
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
       
            app.UseAuthorization();
            app.UseEndpoints(
                endpoint => {
                    endpoint.MapDefaultControllerRoute() ;
                }
            );
        }
    }
}