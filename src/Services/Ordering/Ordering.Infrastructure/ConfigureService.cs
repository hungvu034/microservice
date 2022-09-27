using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Interfaces;
using Ordering.Infrastructure.Persisten;
using Ordering.Infrastructure.Repository;

namespace Ordering.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services , IConfiguration config){
            services.AddDbContext<OrderingContext>(
                options => options.UseSqlServer(config.GetConnectionString("DefaultConnectionString")
            ));
            services.AddScoped<OrderingSeedingContext>();
            services.AddScoped(typeof(IUnitofWork<>) , typeof(UnitOfWork<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services ; 
        }
    }
}