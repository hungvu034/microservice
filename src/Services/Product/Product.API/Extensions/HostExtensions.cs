using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Product.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host) 
        where TContext : DbContext 
        {
            using(var scope = host.Services.CreateScope()){
                var logger = scope.ServiceProvider.GetRequiredService<Serilog.ILogger>();
                var context = scope.ServiceProvider.GetRequiredService<TContext>(); 
                try{
                    logger.Fatal("Migration mysql database ") ; 
                    ExcuteMigrations(context);
                }
                catch{
                    logger.Fatal("An error occurred while migrating the mysql database");
                }
            }
            return host ; 
        }
        public static IHost MigrateDatabase<TContext>(this IHost host , Action<TContext , IServiceProvider> builder) where TContext : DbContext{
            using(var scope = host.Services.CreateScope()){
               var Service = scope.ServiceProvider ; 
               var logger = Service.GetRequiredService<Serilog.ILogger>();
               var context = Service.GetRequiredService<TContext>();
               try{
                logger.Fatal("Migrate mysql database") ; 
                ExcuteMigrations(context);  
                builder.Invoke(context , Service) ;
                 logger.Fatal("Seeded mysql database") ; 
               }
               catch{
                logger.Fatal("An error occurred while migrating the mysql database");
               }
            }
            return host ; 
        }
        private static void ExcuteMigrations(DbContext context) 
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}