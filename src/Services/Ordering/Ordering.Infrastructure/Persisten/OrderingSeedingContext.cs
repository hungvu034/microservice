using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entites;
using Serilog ; 
namespace Ordering.Infrastructure.Persisten
{
    public class OrderingSeedingContext
    {
        private readonly OrderingContext _context ; 
        private readonly ILogger _logger ;

        public OrderingSeedingContext(OrderingContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitialiseAsync(){
            try{
                if(_context.Database.IsSqlServer()){
                    await _context.Database.MigrateAsync();
                }
            }
            catch{
                _logger.Error("An exception throw occurred while initialise the database");
                throw;
            }
        }
        public async Task SeedAsync(){
            try{
                await TrySeedAsync();
                _context.SaveChanges();
            }
            catch{
                _logger.Error("An exception throw occurred while seeding the database");
                throw  ;
            }
        }
        public async Task TrySeedAsync(){
            if(!_context.Orders.Any()){
               await _context.Set<Order>().AddRangeAsync(
                new Order("hungvu" , 20  , "hung" , "vu" , "hungvu034@gmail.com" , "hanoi") , 
                new Order("lenam" , 21  , "nam" , "le" , "lenam@gmail.com" , "haiphong") 
               );
            }
        }
    }
}