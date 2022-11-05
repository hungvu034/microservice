using System.Security.Cryptography.X509Certificates;
using Contracts.Common.Interfaces;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entites;
using Serilog;

namespace Ordering.Infrastructure.Persisten
{
    public class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions<OrderingContext> options, IMediator mediator, ILogger logger) : base(options)
        {
            _mediator = mediator;
            _logger = logger;
        }
        ILogger _logger ; 
        public DbSet<Order> Orders { get; set; }
        private IMediator _mediator ;  
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entites = this.ChangeTracker.Entries<IEventEntity>()
                                            .Select(x => x.Entity)
                                            .Where(x => x.DomainEvents.Any());
            var domainEvents = entites.SelectMany( x => x.DomainEvents).ToArray();
            foreach(var item in entites){
                item.ClearDomainEvent();
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventAsync(domainEvents);
            return result ;
        }
    }
}