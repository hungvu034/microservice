using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iventory.Product.API.Services;
using Iventory.Product.API.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDatabaseSettings = Shared.Configurations.MongoDatabaseSettings ;
namespace Iventory.Product.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            services.AddConfigurationSettings();
            services.AddScoped<IInventoryService,InventoryService>();
            services.ConfigureMongoClient();
        }
        private static void AddConfigurationSettings(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            var dbSettings = configuration.GetSection("DatabaseSettings").Get<MongoDatabaseSettings>();
            services.AddSingleton(dbSettings);
        }
        private static string GetMongoConnectionString(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            var dbSettings = configuration.GetSection("DatabaseSettings").Get< Shared.Configurations.MongoDatabaseSettings>();
            string connectionString = dbSettings.ConnectionString + "/" + dbSettings.DatabaseName + "?authSource=admin";
            return connectionString ; 
        }
        public static void ConfigureMongoClient(this IServiceCollection services)
        {
            var connectionString = services.GetMongoConnectionString();
            services.AddSingleton<IMongoClient>(
                new MongoClient(connectionString)
            );
            //.AddScoped(x => x.GetService<IMongoClient>()?.StartSession());
        }
    }
}