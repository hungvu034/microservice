using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Grpc.Repositories;
using Inventory.Grpc.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDatabaseSettings = Shared.Configurations.MongoDatabaseSettings;
namespace Inventory.Grpc.Extensions
{
    public static class InfrastructorExtensions
    {
        public static IServiceCollection AddInfrastructorExtensions(this IServiceCollection collection)
        {
            collection.AddSingleton<IMongoClient>(
                new MongoClient(GetMongoConnection(collection))
            );
            collection.AddSingleton(GetMongoDbSettings(collection));
            collection.AddScoped<IInventoryRepository,InventoryRepository>();
            return collection;
        }

        public static MongoDatabaseSettings GetMongoDbSettings(this IServiceCollection collection)
        {
            var provider = collection.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            var settings = config.GetSection(nameof(MongoDatabaseSettings)).Get<MongoDatabaseSettings>();
            return settings ; 
        }
        private static string GetMongoConnection(this IServiceCollection collection)
        {
            var provider = collection.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            var settings = config.GetSection(nameof(MongoDatabaseSettings)).Get<MongoDatabaseSettings>();
            string connectionString = settings.ConnectionString + "/" + settings.DatabaseName + "?authSource=admin";
            return connectionString;
        }
    }
}