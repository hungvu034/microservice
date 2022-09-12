using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Product.API.Persistence;
using MySQL.Data.EntityFrameworkCore ;
using Pomelo.EntityFrameworkCore.MySql ;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Infrastructure.Common ;
using Product.API.Entities;
using Contracts.Common.Interfaces;
using Product.API.Repositories;
using Product.API.Repositories.Interfaces;

namespace Product.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration){
            services.AddControllers();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true); 
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.ConfigeProductDbContext(configuration);
            services.ConfigeProductService();
            return services ;
        }

        public static IServiceCollection ConfigeProductDbContext(this IServiceCollection service , IConfiguration config){
            var ConnectionString = config.GetConnectionString("DefaultConnectionStrings");
            var builder = new MySqlConnectionStringBuilder(ConnectionString);
            service.AddDbContext<ProductContext>(m => m.UseMySql(builder.ConnectionString, 
            ServerVersion.AutoDetect(builder.ConnectionString), e =>
        {
            e.MigrationsAssembly("Product.API");
            e.SchemaBehavior(MySqlSchemaBehavior.Ignore);
        }));
            
            return service ; 
        }
        public static IServiceCollection ConfigeProductService(this IServiceCollection service){
            service.AddScoped(typeof(IUnitofWork<>) , typeof(UnitOfWork<>)) ; 
           // service.AddScoped(typeof(IRepositoryBaseAsync<,,>) , typeof(RepositoryBaseAsync<,,>));
            service.AddScoped<IProductRepository,ProductRepository>();
            service.AddAutoMapper(config => config.AddProfile(new MappingProfile()));
            return service ; 
        }
    }
}